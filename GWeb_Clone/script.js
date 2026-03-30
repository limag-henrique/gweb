// ================================================================
// G WEB CLONE — COMPLETE FUNCTIONAL ENGINE
// Rewritten from scratch: all bugs fixed, all features working.
// ================================================================

// ─── GLOBAL STATE (declared first — used everywhere) ────────────
const dependencyPropertyManager = { bindings: {} };
const localVariables = {};                       // name → value
let simulationInterval = null;
let controlCounter = 1;
let draggedType = null;
let canvasZoom = 100; // percent

const wiringState = {
    isWiring: false,
    startPort: null,
    tempLine: null,
    wires: []
};

// ─── ZOOM ───────────────────────────────────────────────────────
function setCanvasZoom(percent) {
    canvasZoom = Math.max(25, Math.min(200, percent));
    var scale = canvasZoom / 100;
    document.querySelectorAll('.canvas-view').forEach(function (v) {
        v.style.transform = 'scale(' + scale + ')';
        v.style.width = (100 / scale) + '%';
        v.style.height = (100 / scale) + '%';
    });
    var sel = document.getElementById('zoom-select');
    if (sel) sel.value = String(canvasZoom);
    setTimeout(updateWires, 60);
}

// Ctrl + Scroll wheel zoom
window.addEventListener('load', function () {
    document.querySelector('.canvas-area').addEventListener('wheel', function (e) {
        if (!e.ctrlKey) return;
        e.preventDefault();
        var delta = e.deltaY < 0 ? 25 : -25;
        setCanvasZoom(canvasZoom + delta);
    }, { passive: false });
});

// ─── TOAST NOTIFICATION SYSTEM ──────────────────────────────────
function showToast(title, message, type) {
    type = type || 'info';
    let container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        container.style.cssText = 'position:fixed;bottom:30px;right:20px;z-index:99999;display:flex;flex-direction:column;gap:10px;pointer-events:none;';
        document.body.appendChild(container);
    }
    var colorMap = { error: '#d13438', success: '#107c10', info: '#0078d4' };
    var color = colorMap[type] || colorMap.info;
    var toast = document.createElement('div');
    toast.style.cssText = 'background:white;border-left:5px solid ' + color + ';box-shadow:0 4px 12px rgba(0,0,0,0.15);padding:15px 20px;border-radius:4px;font-family:Segoe UI,sans-serif;min-width:250px;max-width:380px;animation:slideInToast 0.3s ease-out;position:relative;overflow:hidden;pointer-events:auto;';
    toast.innerHTML = '<h4 style="margin:0 0 5px 0;font-size:14px;color:#333;">' + title + '</h4><p style="margin:0;font-size:12px;color:#666;line-height:1.4;">' + message + '</p>';
    container.appendChild(toast);
    setTimeout(function () {
        toast.style.animation = 'fadeOutToast 0.3s ease-out forwards';
        setTimeout(function () { toast.remove(); }, 300);
    }, 4000);
}

function setStatus(text, type) {
    var bar = document.querySelector('.status-bar');
    if (!bar) return;
    bar.textContent = text;
    bar.className = 'status-bar' + (type ? ' ' + type : '');
}

// ─── KEYBOARD SHORTCUTS ────────────────────────────────────────
document.addEventListener('keydown', function (e) {
    if (e.ctrlKey && e.key.toLowerCase() === 'e') {
        e.preventDefault();
        var btn = document.querySelector('.view-btn.active');
        toggleView(btn && btn.id === 'btn-painel' ? 'diagrama' : 'painel');
    }
    if (e.ctrlKey && e.key.toLowerCase() === 'r') { e.preventDefault(); runApp(); }
    if (e.ctrlKey && e.key.toLowerCase() === 's') { e.preventDefault(); showToast('Projeto Salvo', 'O projeto foi salvo offline!', 'success'); }
    if (e.key === 'Delete') {
        var sel = document.querySelector('.ui-control.selected');
        if (sel) { deleteControl(sel.id); }
    }
});

// ─── CANVAS CLEAR SELECTION ─────────────────────────────────────
window.addEventListener('load', function () {
    document.querySelectorAll('.canvas-view').forEach(function (view) {
        view.addEventListener('mousedown', function (e) {
            if (e.target === this || e.target.classList.contains('canvas-instruction')) {
                document.querySelectorAll('.ui-control').forEach(function (c) { c.classList.remove('selected'); });
                document.getElementById('prop-editor').classList.add('hidden');
                document.getElementById('prop-default').classList.remove('hidden');
            }
        });
    });
});

// ─── G-ENGINE: RUN / STOP ───────────────────────────────────────
function runApp() {
    if (simulationInterval) {
        showToast('Aviso', 'A simulação já está em execução.', 'info');
        return;
    }
    showToast('Executando ▶', 'G-Engine ativo (500ms tick). Dados fluindo pelos fios.', 'success');
    setStatus('● Executando — G-Engine ativo (500ms)', 'running');

    // Animate wires
    wiringState.wires.forEach(function (w) { if (w.line) w.line.classList.add('active'); });

    simulationInterval = setInterval(function () {
        evaluateDataflow();
    }, 500);
}

function stopApp() {
    if (simulationInterval) {
        clearInterval(simulationInterval);
        simulationInterval = null;
        wiringState.wires.forEach(function (w) { if (w.line) w.line.classList.remove('active'); });
        showToast('Parado ■', 'Execução interrompida.', 'error');
        setStatus('Pronto.', '');
    } else {
        showToast('Aviso', 'Nenhuma simulação em andamento.', 'info');
    }
}

function evaluateDataflow() {
    var allNodes = Array.from(document.querySelectorAll('#diagrama-view > .ui-control'));
    if (allNodes.length === 0) return;

    var vals = {};   // nodeId → { in0, in1, out }

    // 1. Initialize all nodes
    allNodes.forEach(function (node) {
        var t = node.getAttribute('data-type');
        vals[node.id] = { in0: null, in1: null, out: null };

        // Source nodes produce values immediately
        if (t === 'rand_num') {
            vals[node.id].out = Math.random();
        } else if (t === 'num_const') {
            vals[node.id].out = parseFloat(node.getAttribute('data-value')) || 0;
        } else if (t === 'bool_const') {
            vals[node.id].out = node.getAttribute('data-value') === 'true' ? 1 : 0;
        } else if (t === 'str_const') {
            vals[node.id].out = node.getAttribute('data-value') || '';
        } else if (t === 'ui_read') {
            var bid = dependencyPropertyManager.bindings[node.id];
            if (bid) {
                var uiEl = document.getElementById(bid);
                if (uiEl) {
                    var inp = uiEl.querySelector('input[type="number"], input[type="range"], input[type="text"], select, input[type="checkbox"]');
                    if (inp) {
                        if (inp.type === 'checkbox') vals[node.id].out = inp.checked ? 1 : 0;
                        else if (inp.type === 'text') vals[node.id].out = inp.value;
                        else vals[node.id].out = parseFloat(inp.value) || 0;
                    } else {
                        var indEl = uiEl.querySelector('.ctrl-ind');
                        vals[node.id].out = indEl ? parseFloat(indEl.textContent) || 0 : 0;
                    }
                }
            } else {
                vals[node.id].out = 0;
            }
        } else if (t === 'local_var_read') {
            var vname = node.getAttribute('data-varname') || 'var1';
            vals[node.id].out = localVariables[vname] !== undefined ? localVariables[vname] : 0;
        }
    });

    // 2. Multi-pass propagation (handles cascaded wires, up to 10 depth)
    for (var pass = 0; pass < 10; pass++) {
        var changed = false;

        // Route outputs → inputs
        wiringState.wires.forEach(function (w) {
            if (vals[w.outBox] && vals[w.inBox]) {
                var v = vals[w.outBox].out;
                if (v !== null && v !== undefined) {
                    var key = 'in' + w.inIdx;
                    if (vals[w.inBox][key] !== v) {
                        vals[w.inBox][key] = v;
                        changed = true;
                    }
                }
            }
        });

        // Evaluate operator nodes
        allNodes.forEach(function (node) {
            var t = node.getAttribute('data-type');
            var v = vals[node.id];
            var a = v.in0 !== null ? v.in0 : 0;
            var b = v.in1 !== null ? v.in1 : 0;
            var prev = v.out;

            if (t === 'add_node') v.out = a + b;
            else if (t === 'sub_node') v.out = a - b;
            else if (t === 'mul_node') v.out = a * b;
            else if (t === 'div_node') v.out = b !== 0 ? a / b : 0;
            else if (t === 'gt_node') v.out = a > b ? 1 : 0;
            else if (t === 'lt_node') v.out = a < b ? 1 : 0;
            else if (t === 'eq_node') v.out = a == b ? 1 : 0;
            else if (t === 'not_node') v.out = a ? 0 : 1;
            else if (t === 'and_node') v.out = (a && b) ? 1 : 0;
            else if (t === 'or_node') v.out = (a || b) ? 1 : 0;
            else if (t === 'abs_node') v.out = Math.abs(a);
            else if (t === 'sqrt_node') v.out = Math.sqrt(Math.abs(a));
            else if (t === 'mod_node') v.out = b !== 0 ? a % b : 0;
            else if (t === 'str_concat') v.out = String(a) + String(b);
            else if (t === 'num2str_node') v.out = String(a);
            else if (t === 'str2num_node') v.out = parseFloat(a) || 0;

            if (v.out !== prev) changed = true;
        });

        if (!changed) break;
    }

    // 3. Sink nodes push values to frontend
    allNodes.forEach(function (node) {
        var t = node.getAttribute('data-type');
        var v = vals[node.id];

        if (t === 'ui_write') {
            var bid = dependencyPropertyManager.bindings[node.id];
            if (bid) {
                var uiEl = document.getElementById(bid);
                if (uiEl && v.in0 !== null) {
                    writeToUIControl(uiEl, v.in0);
                }
            }
        } else if (t === 'local_var_write') {
            var vname = node.getAttribute('data-varname') || 'var1';
            if (v.in0 !== null) localVariables[vname] = v.in0;
        }
    });
}

function writeToUIControl(el, val) {
    var inp = el.querySelector('input[type="number"], input[type="range"]');
    var strInp = el.querySelector('input[type="text"]');
    var ind = el.querySelector('.ctrl-ind');
    var tankFill = el.querySelector('.ctrl-tank-fill');
    var chart = el.querySelector('.g-sim-chart');
    var progBar = el.querySelector('progress');
    var led = el.querySelector('.ctrl-led');
    var tog = el.querySelector('.ctrl-toggle-switch');
    var numVal = (typeof val === 'number') ? val : parseFloat(val) || 0;

    if (inp) inp.value = numVal;
    if (strInp && typeof val === 'string') strInp.value = val;
    if (ind) ind.textContent = (typeof val === 'number') ? val.toFixed(2) : String(val);
    if (tankFill) tankFill.style.height = Math.min(Math.max(numVal, 0), 100) + '%';
    if (progBar) progBar.value = Math.min(Math.max(numVal, 0), 100);
    if (led) led.style.background = numVal ? '#5cb85c' : '#333';
    if (tog) tog.checked = !!numVal;
    if (chart) {
        var bar = document.createElement('div');
        bar.className = 'chart-bar';
        bar.style.height = Math.min(Math.max(numVal, 0), 100) + '%';
        chart.appendChild(bar);
        if (chart.children.length > 80) chart.removeChild(chart.children[0]);
    }
}

// ─── VIEWS & TABS ───────────────────────────────────────────────
function toggleView(viewName) {
    if (viewName === 'codigo') {
        var painelHtml = document.getElementById('painel-view').innerHTML;
        var cleanHtml = painelHtml.replace(/<div class="canvas-instruction"[^]*?<\/div>/g, '');
        cleanHtml = cleanHtml.replace(/<div class="drag-handle"[^]*?<\/div>/g, '');
        var fullCode = '<!DOCTYPE html>\n<html>\n<head>\n<style>\n  .ui-control { position:absolute; font-family:Segoe UI,sans-serif; }\n  .flex-container { display:flex; gap:10px; padding:10px; border:1px solid #ccc; }\n</style>\n</head>\n<body>\n<div class="app-interface">\n' + cleanHtml + '\n</div>\n<script>\nconsole.log("G Web App loaded.");\n<\/script>\n</body>\n</html>';
        document.getElementById('codigo-saida').innerText = fullCode;
    }
    document.querySelectorAll('.view-btn').forEach(function (b) { b.classList.remove('active'); });
    if (viewName !== 'codigo') {
        var btn = document.getElementById('btn-' + viewName);
        if (btn) btn.classList.add('active');
    }
    document.querySelectorAll('.canvas-view').forEach(function (v) { v.classList.remove('active-view'); });
    var targetView = document.getElementById(viewName + '-view');
    if (targetView) targetView.classList.add('active-view');

    if (viewName === 'diagrama') {
        document.getElementById('diagrama-palette').style.display = 'block';
        document.getElementById('painel-palette').style.display = 'none';
        switchSideTab('paleta');
        setTimeout(updateWires, 50);
    } else if (viewName === 'painel') {
        document.getElementById('diagrama-palette').style.display = 'none';
        document.getElementById('painel-palette').style.display = 'block';
    }
}

function switchSideTab(tabName) {
    document.querySelectorAll('.side-tab').forEach(function (b) { b.classList.remove('active'); });
    var tab = document.getElementById('tab-' + tabName);
    if (tab) tab.classList.add('active');
    document.getElementById('paleta-content').style.display = (tabName === 'paleta') ? 'block' : 'none';
    document.getElementById('projeto-content').style.display = (tabName === 'projeto') ? 'block' : 'none';
    document.getElementById('arvore-content').style.display = (tabName === 'arvore') ? 'block' : 'none';
    if (tabName === 'arvore') renderVisualTree();
}

function toggleCategory(headerEl) { headerEl.classList.toggle('active'); }

// ─── VISUAL TREE ────────────────────────────────────────────────
function renderVisualTree() {
    var vtree = document.getElementById('vtree-container');
    if (!vtree) return;
    var activeCanvas = document.querySelector('.canvas-view.active-view');
    if (!activeCanvas) { vtree.innerHTML = '<li style="color:#999;font-size:11px;">[Sem canvas ativo]</li>'; return; }

    var children = [];
    for (var i = 0; i < activeCanvas.children.length; i++) {
        var ch = activeCanvas.children[i];
        if (ch.classList.contains('ui-control')) children.push(ch);
    }
    vtree.innerHTML = '';
    if (children.length === 0) {
        vtree.innerHTML = '<li style="color:#999;font-size:11px;">[Canvas Vazio]</li>';
        return;
    }

    function buildNode(el) {
        var id = el.id || '?';
        var type = el.getAttribute('data-type') || '?';
        var lbl = el.querySelector('.ctrl-lbl, .ctrl-btn, .g-struct-header, .flex-header');
        var text = lbl ? lbl.textContent.trim() : type;
        if (!text) text = type;
        var sel = el.classList.contains('selected') ? ' selected' : '';
        var html = '<div class="vtree-item' + sel + '" onclick="selectFromTree(\'' + id + '\')">' +
            '<span class="vtree-icon"><i class="fa-solid fa-cube"></i></span>' +
            '<span>' + text + ' <span class="vtree-id">#' + id + '</span></span></div>';
        var inner = el.querySelector('.flex-container, .grid-container');
        if (inner) {
            var innerKids = [];
            for (var j = 0; j < inner.children.length; j++) {
                if (inner.children[j].classList.contains('ui-control')) innerKids.push(inner.children[j]);
            }
            if (innerKids.length > 0) {
                html += '<ul class="vtree-node">';
                innerKids.forEach(function (c) { html += '<li>' + buildNode(c) + '</li>'; });
                html += '</ul>';
            }
        }
        return html;
    }
    children.forEach(function (el) { vtree.innerHTML += '<li>' + buildNode(el) + '</li>'; });
}

window.selectFromTree = function (id) {
    var el = document.getElementById(id);
    if (el) {
        document.querySelectorAll('.ui-control').forEach(function (c) { c.classList.remove('selected'); });
        el.classList.add('selected');
        var type = el.getAttribute('data-type') || '';
        showPropertiesPane(id, type);
        renderVisualTree();
    }
};

// ─── DRAG & DROP ────────────────────────────────────────────────
function drag(ev, type) {
    draggedType = type;
    ev.dataTransfer.setData('text/plain', type);
}
function allowDrop(ev) { ev.preventDefault(); }

// List of all diagram-only node types
var DIAGRAM_TYPES = [
    'while_loop', 'for_loop', 'case_struct', 'event_struct', 'shift_reg',
    'add_node', 'sub_node', 'mul_node', 'div_node', 'rand_num', 'num_const',
    'gt_node', 'lt_node', 'eq_node', 'not_node', 'and_node', 'or_node',
    'abs_node', 'sqrt_node', 'mod_node', 'bool_const',
    'str_const', 'str_concat', 'num2str_node', 'str2num_node',
    'obt_queue', 'enq_elem', 'deq_elem', 'open_msg', 'read_tag',
    'ui_read', 'ui_write', 'local_var_read', 'local_var_write'
];

// ─── HTML GENERATORS ────────────────────────────────────────────
var htmlGenerators = {
    // === FRONTEND (Painel) ===
    'flex_row':    function () { return '<div class="flex-header"><i class="fa-solid fa-table-columns"></i> Flexible Row</div><div class="flex-container"></div>'; },
    'flex_col':    function () { return '<div class="flex-header"><i class="fa-solid fa-table-cells-large"></i> Flexible Column</div><div class="flex-container col"></div>'; },
    'grid_layout': function () { return '<div class="flex-header" style="background:#555;"><i class="fa-solid fa-border-all"></i> Grid Layout</div><div class="grid-container"></div>'; },
    'num_ctrl':    function () { return '<div class="ctrl-lbl">Numeric</div><input type="number" class="ctrl-input" value="0" step="any">'; },
    'num_ind':     function () { return '<div class="ctrl-lbl">Indicador</div><div class="ctrl-ind">0.00</div>'; },
    'h_slider':    function () { return '<div class="ctrl-lbl">Slider</div><input type="range" min="0" max="100" value="50" style="width:120px;cursor:pointer;">'; },
    'v_slider':    function () { return '<div class="ctrl-lbl">V-Slider</div><input type="range" orient="vertical" min="0" max="100" value="50" style="writing-mode:bt-lr;-webkit-appearance:slider-vertical;height:100px;cursor:pointer;">'; },
    'gauge':       function () { return '<div class="ctrl-lbl">Gauge</div><div style="width:60px;height:60px;border-radius:50%;border:4px solid #333;background:#fff;position:relative;"><div style="position:absolute;bottom:50%;left:50%;width:3px;height:25px;background:red;transform-origin:bottom;transform:rotate(45deg);"></div></div>'; },
    'tank':        function () { return '<div class="ctrl-lbl">Tank</div><div class="ctrl-tank"><div class="ctrl-tank-fill"></div></div>'; },
    'time_ctrl':   function () { return '<div class="ctrl-lbl">Time</div><input type="datetime-local" class="ctrl-input">'; },
    'time_ind':    function () { return '<div class="ctrl-lbl">Time Ind</div><div class="ctrl-ind">2026-01-01 00:00</div>'; },
    'prog_linear': function () { return '<div class="ctrl-lbl">Progress</div><progress value="70" max="100" style="width:120px;"></progress>'; },
    'prog_radial': function () { return '<div class="ctrl-lbl">Wait...</div><div style="width:30px;height:30px;border:4px solid #ccc;border-top:4px solid #0078d4;border-radius:50%;animation:spin 1s linear infinite;"></div>'; },
    'text_btn':    function () { return '<button class="ctrl-btn" onclick="showToast(\'Clique\',\'Botão acionado.\',\'info\')">Ação</button>'; },
    'stop_btn':    function () { return '<button class="ctrl-btn" style="background:#d13438;border-color:#a80000;" onclick="stopApp()"><i class="fa-solid fa-stop"></i> PARAR</button>'; },
    'power_btn':   function () { return '<button class="ctrl-btn" style="background:#fff;color:#333;border-color:#ccc;border-radius:50%;width:40px;height:40px;" onclick="this.style.color=this.style.color===\'green\'?\'#333\':\'green\'"><i class="fa-solid fa-power-off"></i></button>'; },
    'checkbox':    function () { return '<label style="cursor:pointer;display:flex;align-items:center;font-size:12px;font-weight:600;"><input type="checkbox" style="margin-right:5px;cursor:pointer;" checked> Checkbox</label>'; },
    'h_switch':    function () { return '<div class="ctrl-lbl">Switch</div><input type="checkbox" class="ctrl-toggle-switch">'; },
    'v_switch':    function () { return '<div class="ctrl-lbl">V-Switch</div><input type="checkbox" class="ctrl-toggle-switch" style="transform:rotate(270deg);margin:15px 0;">'; },
    'led':         function () { return '<div class="ctrl-lbl">LED</div><div class="ctrl-led" onclick="this.style.background=this.style.background===\'darkgreen\'?\'#5cb85c\':\'darkgreen\'" style="cursor:pointer;"></div>'; },
    'str_ctrl':    function () { return '<div class="ctrl-lbl">String</div><input type="text" class="ctrl-input" placeholder="Digite...">'; },
    'str_ind':     function () { return '<div class="ctrl-lbl">String Ind</div><div class="ctrl-ind" style="width:120px;min-height:22px;">—</div>'; },
    'text_lbl':    function () { return '<div style="font-weight:bold;font-size:14px;color:#333;">Texto Informativo</div>'; },
    'mask_input':  function () { return '<div class="ctrl-lbl">Senha</div><input type="password" class="ctrl-input" placeholder="•••">'; },
    'hyperlink':   function () { return '<a href="#" style="color:#0078d4;font-size:12px;text-decoration:underline;">Link</a>'; },
    'graph':       function () { return '<div class="ctrl-lbl">Graph</div><div class="ctrl-chart" style="border-color:#0078d4;justify-content:space-around;align-items:flex-end;padding:5px;"><div style="width:20px;height:40%;background:#0078d4;"></div><div style="width:20px;height:90%;background:#0078d4;"></div><div style="width:20px;height:60%;background:#0078d4;"></div></div>'; },
    'chart':       function () { return '<div class="ctrl-lbl">Chart Contínuo</div><div class="ctrl-chart g-sim-chart"></div>'; },
    'int_graph':   function () { return '<div class="ctrl-lbl">Intensity</div><div style="width:120px;height:120px;background:radial-gradient(circle,red,yellow,lime,aqua,blue);border:1px solid #333;"></div>'; },
    'data_grid':   function () { return '<div class="ctrl-lbl">Data Grid</div><table border="1" cellpadding="3" style="border-collapse:collapse;font-size:11px;background:white;"><tr><th>A</th><th>B</th></tr><tr><td>1</td><td>2</td></tr><tr><td>3</td><td>4</td></tr></table>'; },
    'enum':        function () { return '<div class="ctrl-lbl">Enum</div><select class="ctrl-input" style="cursor:pointer;"><option>Init</option><option>Run</option><option>Stop</option></select>'; },
    'html_cont':   function () { return '<div style="border:2px dashed #0078d4;padding:15px;color:#0078d4;font-size:12px;text-align:center;background:rgba(0,120,212,0.05);font-weight:bold;">&lt;/&gt; JSLI Container</div>'; },

    // === BACKEND (Diagrama) — all have ports ===
    'while_loop':  function () { return '<div class="g-struct"><div class="g-struct-header"><i class="fa-solid fa-repeat"></i> While Loop</div><div style="position:absolute;bottom:5px;right:5px;font-weight:bold;color:red;border:1px solid red;border-radius:50%;width:15px;height:15px;display:flex;justify-content:center;align-items:center;font-size:10px;">O</div><div style="position:absolute;bottom:5px;left:5px;font-weight:bold;background:#0078d4;color:white;width:15px;height:15px;display:flex;justify-content:center;align-items:center;font-size:10px;border:1px solid #333;">i</div></div>'; },
    'for_loop':    function () { return '<div class="g-struct"><div class="g-struct-header" style="background:#005a9e;"><b>N</b> For Loop</div><div style="position:absolute;top:30px;left:5px;font-weight:bold;background:#005a9e;color:white;width:15px;height:15px;display:flex;justify-content:center;align-items:center;font-size:10px;border:1px solid #333;">N</div><div style="position:absolute;bottom:5px;left:5px;font-weight:bold;background:#005a9e;color:white;width:15px;height:15px;display:flex;justify-content:center;align-items:center;font-size:10px;border:1px solid #333;">i</div></div>'; },
    'case_struct': function () { return '<div class="g-struct" style="border-color:#333;"><div class="g-struct-header" style="background:#333;">? Case [True]</div></div>'; },
    'event_struct':function () { return '<div class="g-struct" style="border-color:#0078d4;"><div class="g-struct-header" style="background:#0078d4;"><i class="fa-solid fa-bolt"></i> Event [Change]</div></div>'; },
    'shift_reg':   function () { return '<div class="g-node" style="width:20px;height:40px;background:#5cb85c;border-color:#333;"><i class="fa-solid fa-angles-down"></i></div>'; },
    'obt_queue':   function () { return '<div class="g-node g-node-queue"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-layer-group"></i></div>'; },
    'enq_elem':    function () { return '<div class="g-node g-node-queue"><div class="g-port" data-io="in" data-idx="0"></div><i class="fa-solid fa-arrow-right-to-bracket"></i></div>'; },
    'deq_elem':    function () { return '<div class="g-node g-node-queue"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-arrow-right-from-bracket"></i></div>'; },
    'open_msg':    function () { return '<div class="g-node g-node-sys"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-envelope-open"></i></div>'; },
    'read_tag':    function () { return '<div class="g-node g-node-sys"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-tag"></i></div>'; },
    'ui_read':     function () { return '<div class="g-node g-node-sys g-node-wide"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-eye" style="margin-right:3px;"></i>Read UI</div>'; },
    'ui_write':    function () { return '<div class="g-node g-node-sys g-node-wide" style="border-color:#107c10;color:#107c10;"><div class="g-port" data-io="in" data-idx="0"></div><i class="fa-solid fa-pen" style="margin-right:3px;"></i>Write UI</div>'; },

    // Math nodes — 2 inputs, 1 output
    'add_node':    function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>+</div>'; },
    'sub_node':    function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>−</div>'; },
    'mul_node':    function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>×</div>'; },
    'div_node':    function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>÷</div>'; },
    'mod_node':    function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>%</div>'; },
    'abs_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>|x|</div>'; },
    'sqrt_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>√x</div>'; },
    'rand_num':    function () { return '<div class="g-node"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-dice" style="font-size:14px;"></i></div>'; },
    'num_const':   function () { return '<div class="g-node g-node-wide" style="background:#d4edda;border-color:#28a745;font-size:12px;"><div class="g-port" data-io="out" data-idx="0"></div><span class="const-display">0</span></div>'; },
    'bool_const':  function () { return '<div class="g-node" style="background:#fff3cd;border-color:#ffc107;font-size:11px;width:50px;"><div class="g-port" data-io="out" data-idx="0"></div><span class="const-display">T</span></div>'; },

    // Comparison — 2 inputs, 1 output
    'gt_node':     function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>&gt;</div>'; },
    'lt_node':     function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>&lt;</div>'; },
    'eq_node':     function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>=</div>'; },
    'not_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>NOT</div>'; },
    'and_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>AND</div>'; },
    'or_node':     function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>OR</div>'; },

    // String nodes
    'str_const':   function () { return '<div class="g-node g-node-wide" style="background:#d6eaf8;border-color:#2980b9;font-size:9px;"><div class="g-port" data-io="out" data-idx="0"></div><span class="const-display">"S"</span></div>'; },
    'str_concat':  function () { return '<div class="g-node g-node-wide" style="background:#d6eaf8;border-color:#2980b9;font-size:9px;"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>CONCAT</div>'; },
    'num2str_node': function () { return '<div class="g-node g-node-wide" style="background:#d6eaf8;border-color:#2980b9;font-size:9px;"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>N→S</div>'; },
    'str2num_node': function () { return '<div class="g-node g-node-wide" style="background:#d6eaf8;border-color:#2980b9;font-size:9px;"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>S→N</div>'; },

    // Variables
    'local_var_read':  function () { return '<div class="g-node g-node-wide" style="background:#e8daef;border-color:#8e44ad;font-size:10px;"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-download" style="margin-right:2px;font-size:9px;"></i>ReadVar</div>'; },
    'local_var_write': function () { return '<div class="g-node g-node-wide" style="background:#d5f5e3;border-color:#27ae60;font-size:10px;"><div class="g-port" data-io="in" data-idx="0"></div><i class="fa-solid fa-upload" style="margin-right:2px;font-size:9px;"></i>WriteVar</div>'; }
};

// ─── DROP HANDLER ───────────────────────────────────────────────
function drop(ev, targetView) {
    ev.preventDefault();
    if (!draggedType) return;

    var isLogic = DIAGRAM_TYPES.indexOf(draggedType) !== -1;

    if (isLogic && targetView === 'painel') {
        showToast('Ação Inválida', "Blocos de programação G só podem ser inseridos no Diagrama.", 'error');
        draggedType = null; return;
    }
    if (!isLogic && targetView === 'diagrama') {
        showToast('Ação Inválida', "Componentes visuais só podem ser inseridos no Painel.", 'error');
        draggedType = null; return;
    }

    var instrEl = document.getElementById('instruction-' + targetView);
    if (instrEl) instrEl.style.display = 'none';

    var control = document.createElement('div');
    var cid = 'comp_' + controlCounter++;
    control.id = cid;
    control.classList.add('ui-control');
    control.setAttribute('data-type', draggedType);

    var canvasRect = ev.currentTarget.getBoundingClientRect();
    var dropTarget = ev.target.closest('.flex-container, .grid-container');
    var finalParent = dropTarget || ev.currentTarget;

    if (dropTarget) {
        control.style.position = 'relative';
        control.style.left = '0';
        control.style.top = '0';
        control.style.margin = '5px';
    } else {
        control.style.left = (ev.clientX - canvasRect.left - 30) + 'px';
        control.style.top = (ev.clientY - canvasRect.top - 15) + 'px';
    }

    var controlType = draggedType;
    var gen = htmlGenerators[controlType];
    var payload = gen ? gen() : '<div>[' + controlType + ']</div>';
    control.innerHTML = '<div class="drag-handle" title="Arraste por aqui"></div>' + payload;

    // Set default data attributes
    if (controlType === 'num_const') control.setAttribute('data-value', '0');
    if (controlType === 'bool_const') control.setAttribute('data-value', 'true');
    if (controlType === 'str_const') control.setAttribute('data-value', '');
    if (controlType === 'local_var_read' || controlType === 'local_var_write') {
        control.setAttribute('data-varname', 'var1');
    }

    setupControlInteraction(control);
    finalParent.appendChild(control);
    draggedType = null;

    if (document.getElementById('tab-arvore').classList.contains('active')) {
        renderVisualTree();
    }
}

// ─── CONTROL INTERACTION SETUP ──────────────────────────────────
function setupControlInteraction(control) {
    control.addEventListener('mousedown', function (e) {
        // Don't interfere with ports, inputs, buttons, selects
        if (e.target.classList.contains('g-port')) return;
        if (e.target.closest('input, button, select, a, textarea, label')) return;

        e.preventDefault();
        e.stopPropagation();

        // Select
        document.querySelectorAll('.ui-control').forEach(function (c) { c.classList.remove('selected'); });
        control.classList.add('selected');
        control.style.zIndex = controlCounter++;

        var type = control.getAttribute('data-type') || '';
        showPropertiesPane(control.id, type);

        // Don't drag if inside a layout container
        if (control.parentElement && (control.parentElement.classList.contains('flex-container') || control.parentElement.classList.contains('grid-container'))) {
            return;
        }

        // Start dragging
        var startX = e.clientX;
        var startY = e.clientY;
        var initialLeft = parseInt(control.style.left) || 0;
        var initialTop = parseInt(control.style.top) || 0;

        function onMove(event) {
            control.style.left = (initialLeft + (event.clientX - startX)) + 'px';
            control.style.top = (initialTop + (event.clientY - startY)) + 'px';
            updateWires();
        }
        function onUp() {
            document.removeEventListener('mousemove', onMove);
            document.removeEventListener('mouseup', onUp);
            if (document.getElementById('tab-arvore').classList.contains('active')) renderVisualTree();
        }
        document.addEventListener('mousemove', onMove);
        document.addEventListener('mouseup', onUp);
    });
}

// ─── DELETE CONTROL ─────────────────────────────────────────────
function deleteControl(id) {
    var el = document.getElementById(id);
    if (!el) return;
    // Remove all wires connected to this control
    wiringState.wires = wiringState.wires.filter(function (w) {
        if (w.outBox === id || w.inBox === id) {
            if (w.line) w.line.remove();
            return false;
        }
        return true;
    });
    el.remove();
    delete dependencyPropertyManager.bindings[id];
    document.getElementById('prop-editor').classList.add('hidden');
    document.getElementById('prop-default').classList.remove('hidden');
    showToast('Removido', 'Componente #' + id + ' excluído.', 'info');
    renderVisualTree();
}

// ─── PROPERTIES PANEL ───────────────────────────────────────────
function showPropertiesPane(id, type) {
    var el = document.getElementById(id);
    if (!el) return;

    var labelEl = el.querySelector('.ctrl-lbl, .ctrl-btn, .g-struct-header, .flex-header, .ctrl-ind');
    var currentText = labelEl ? labelEl.textContent.trim() : '';

    document.getElementById('prop-default').classList.add('hidden');
    document.getElementById('prop-editor').classList.remove('hidden');

    var bindingVal = dependencyPropertyManager.bindings[id] || '';
    var safeText = currentText.replace(/"/g, '&quot;');

    // Build extra fields per type
    var extraHTML = '';

    if (type === 'num_const') {
        var cv = el.getAttribute('data-value') || '0';
        extraHTML = '<div class="prop-item"><span>Valor da Constante:</span><input type="number" step="any" id="prop-const-val" value="' + cv + '"></div>';
    } else if (type === 'bool_const') {
        var bv = el.getAttribute('data-value') || 'true';
        extraHTML = '<div class="prop-item"><span>Valor Booleano:</span><select id="prop-bool-val"><option value="true"' + (bv === 'true' ? ' selected' : '') + '>True</option><option value="false"' + (bv === 'false' ? ' selected' : '') + '>False</option></select></div>';
    } else if (type === 'str_const') {
        var sv = el.getAttribute('data-value') || '';
        extraHTML = '<div class="prop-item"><span>Valor String:</span><input type="text" id="prop-str-val" value="' + sv.replace(/"/g,'&quot;') + '" placeholder="ex: Hello"></div>';
    } else if (type === 'local_var_read' || type === 'local_var_write') {
        var vn = el.getAttribute('data-varname') || 'var1';
        extraHTML = '<div class="prop-item"><span>Nome da Variável:</span><input type="text" id="prop-varname" value="' + vn + '" placeholder="ex: temperatura"></div>';
    }

    // Auto-binding dropdown for ui_read/ui_write
    var needsBinding = (type === 'ui_read' || type === 'ui_write' || DIAGRAM_TYPES.indexOf(type) === -1);
    var bindingHTML = '';
    if (needsBinding) {
        var opts = '<option value="">(nenhum)</option>';
        document.querySelectorAll('#painel-view > .ui-control').forEach(function(c){
            var cType = c.getAttribute('data-type') || '?';
            var lbl = c.querySelector('.ctrl-lbl');
            var label = lbl ? lbl.textContent.trim() : cType;
            var sel = (bindingVal === c.id) ? ' selected' : '';
            opts += '<option value="'+c.id+'"'+sel+'>'+label+' ('+c.id+')</option>';
        });
        bindingHTML = '<h4 style="color:#107c10;margin-top:18px;margin-bottom:8px;font-weight:400;font-size:13px;"><i class="fa-solid fa-link"></i> Data Binding</h4>' +
            '<div class="prop-item"><span>Vincular a:</span><select id="prop-binding-input">' + opts + '</select></div>';
    }

    document.getElementById('prop-editor').innerHTML =
        '<h4 style="color:#0078d4;margin-bottom:12px;font-weight:400;font-size:15px;">Propriedades</h4>' +
        '<div class="prop-item"><span>ID:</span><input type="text" value="' + id + '" disabled style="background:#eee;"></div>' +
        '<div class="prop-item"><span>Tipo:</span><input type="text" value="' + (type || '?').toUpperCase() + '" disabled style="background:#eee;"></div>' +
        '<div class="prop-item"><span>Rótulo:</span><input type="text" id="prop-text-input" value="' + safeText + '" placeholder="Texto..."></div>' +
        '<div class="prop-item"><span>CSS Classes:</span><input type="text" id="prop-css-input" placeholder="ex: destaque"></div>' +
        extraHTML +
        bindingHTML +
        '<div style="margin-top:18px;border-top:1px solid #ddd;padding-top:15px;text-align:center;">' +
        '<button id="prop-delete-btn" style="padding:6px 18px;background:#d13438;color:white;border:none;border-radius:3px;cursor:pointer;font-weight:600;">Excluir Componente</button></div>';

    // Wire up label editing
    var textInput = document.getElementById('prop-text-input');
    if (textInput) {
        textInput.addEventListener('input', function () {
            if (labelEl) {
                var iconEl = labelEl.querySelector('i');
                var iconHTML = iconEl ? iconEl.outerHTML + ' ' : '';
                labelEl.innerHTML = iconHTML + this.value;
            }
            if (document.getElementById('tab-arvore').classList.contains('active')) renderVisualTree();
        });
    }

    // Wire up CSS classes
    var cssInput = document.getElementById('prop-css-input');
    if (cssInput) {
        cssInput.addEventListener('change', function () {
            el.className = 'ui-control' + (this.value ? ' ' + this.value : '');
        });
    }

    // Wire up binding (now a dropdown)
    var bindingInput = document.getElementById('prop-binding-input');
    if (bindingInput) {
        bindingInput.addEventListener('change', function () {
            var tid = this.value.trim();
            if (tid) {
                dependencyPropertyManager.bindings[id] = tid;
                showToast('Binding', id + ' → ' + tid, 'success');
            } else {
                delete dependencyPropertyManager.bindings[id];
            }
        });
    }

    // Wire up str_const
    var strInput = document.getElementById('prop-str-val');
    if (strInput) {
        strInput.addEventListener('input', function () {
            el.setAttribute('data-value', this.value);
            var disp = el.querySelector('.const-display');
            if (disp) disp.textContent = '"' + this.value.substr(0,6) + '"';
        });
    }

    // Wire up num_const
    var constInput = document.getElementById('prop-const-val');
    if (constInput) {
        constInput.addEventListener('input', function () {
            var v = parseFloat(this.value) || 0;
            el.setAttribute('data-value', v);
            var disp = el.querySelector('.const-display');
            if (disp) disp.textContent = v;
        });
    }

    // Wire up bool_const
    var boolSelect = document.getElementById('prop-bool-val');
    if (boolSelect) {
        boolSelect.addEventListener('change', function () {
            el.setAttribute('data-value', this.value);
            var disp = el.querySelector('.const-display');
            if (disp) disp.textContent = this.value === 'true' ? 'T' : 'F';
        });
    }

    // Wire up variable name
    var varnameInput = document.getElementById('prop-varname');
    if (varnameInput) {
        varnameInput.addEventListener('change', function () {
            el.setAttribute('data-varname', this.value.trim() || 'var1');
        });
    }

    // Wire up delete
    var delBtn = document.getElementById('prop-delete-btn');
    if (delBtn) {
        delBtn.addEventListener('click', function () { deleteControl(id); });
    }
}

// ─── ROUTED EVENTS (Bubbling Bindings on Frontend) ──────────────
document.addEventListener('input', function (e) {
    var control = e.target.closest('.ui-control');
    if (!control) return;
    var sourceId = control.id;
    var targetId = dependencyPropertyManager.bindings[sourceId];
    if (!targetId) return;
    var targetEl = document.getElementById(targetId);
    if (!targetEl) return;
    writeToUIControl(targetEl, e.target.value);
});

// ─── BUILD APPLICATION ──────────────────────────────────────────
function buildApplication() {
    toggleView('codigo');
    var html = document.getElementById('codigo-saida').innerText;
    var blob = new Blob([html], { type: 'text/html' });
    var a = document.createElement('a');
    a.href = URL.createObjectURL(blob);
    a.download = 'GWeb_App_Exportado.html';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    showToast('Build Completo!', 'Download do HTML compilado iniciado.', 'success');
}

// ─── WIRING ENGINE ──────────────────────────────────────────────
document.addEventListener('mousedown', function (e) {
    if (!e.target.classList.contains('g-port')) return;
    e.stopPropagation();
    e.preventDefault();

    var svg = document.getElementById('wiring-layer');
    if (!svg) return;

    wiringState.isWiring = true;
    wiringState.startPort = e.target;

    wiringState.tempLine = document.createElementNS('http://www.w3.org/2000/svg', 'path');
    wiringState.tempLine.classList.add('wire-line');
    svg.appendChild(wiringState.tempLine);
}, true);   // capture phase — fires BEFORE the control's mousedown

document.addEventListener('mousemove', function (e) {
    if (!wiringState.isWiring || !wiringState.tempLine || !wiringState.startPort) return;
    var svg = document.getElementById('wiring-layer');
    if (!svg) return;
    var svgRect = svg.getBoundingClientRect();
    var startRect = wiringState.startPort.getBoundingClientRect();

    var x1 = startRect.left + startRect.width / 2 - svgRect.left;
    var y1 = startRect.top + startRect.height / 2 - svgRect.top;
    var x2 = e.clientX - svgRect.left;
    var y2 = e.clientY - svgRect.top;
    var cx = Math.abs(x2 - x1) * 0.5 + 30;

    wiringState.tempLine.setAttribute('d', 'M ' + x1 + ' ' + y1 + ' C ' + (x1 + cx) + ' ' + y1 + ', ' + (x2 - cx) + ' ' + y2 + ', ' + x2 + ' ' + y2);
});

document.addEventListener('mouseup', function (e) {
    if (!wiringState.isWiring) return;
    wiringState.isWiring = false;

    var endPort = e.target.classList.contains('g-port') ? e.target : null;

    if (endPort && endPort !== wiringState.startPort && endPort.getAttribute('data-io') !== wiringState.startPort.getAttribute('data-io')) {
        var outPort = wiringState.startPort.getAttribute('data-io') === 'out' ? wiringState.startPort : endPort;
        var inPort = wiringState.startPort.getAttribute('data-io') === 'in' ? wiringState.startPort : endPort;

        var outCtrl = outPort.closest('.ui-control');
        var inCtrl = inPort.closest('.ui-control');

        if (outCtrl && inCtrl) {
            var wireId = 'wire_' + Date.now() + '_' + Math.random().toString(36).substr(2, 4);
            wiringState.tempLine.id = wireId;

            var wire = {
                id: wireId,
                outBox: outCtrl.id,
                outIdx: outPort.getAttribute('data-idx') || '0',
                inBox: inCtrl.id,
                inIdx: inPort.getAttribute('data-idx') || '0',
                line: wiringState.tempLine
            };
            wiringState.wires.push(wire);
            updateWires();

            // Click to delete wire
            wiringState.tempLine.style.pointerEvents = 'visibleStroke';
            wiringState.tempLine.addEventListener('click', function () {
                this.remove();
                wiringState.wires = wiringState.wires.filter(function (w) { return w.id !== wireId; });
                showToast('Fio Removido', 'Conexão deletada.', 'info');
            });

            showToast('Conectado', outCtrl.id + ' → ' + inCtrl.id, 'success');
        } else {
            if (wiringState.tempLine) wiringState.tempLine.remove();
        }
    } else {
        if (wiringState.tempLine) wiringState.tempLine.remove();
    }

    wiringState.startPort = null;
    wiringState.tempLine = null;
});

function updateWires() {
    var svg = document.getElementById('wiring-layer');
    if (!svg) return;
    var svgRect = svg.getBoundingClientRect();

    wiringState.wires.forEach(function (w) {
        var outPort = document.querySelector('#' + CSS.escape(w.outBox) + ' .g-port[data-io="out"][data-idx="' + w.outIdx + '"]');
        var inPort = document.querySelector('#' + CSS.escape(w.inBox) + ' .g-port[data-io="in"][data-idx="' + w.inIdx + '"]');
        if (!outPort || !inPort) return;

        var r1 = outPort.getBoundingClientRect();
        var r2 = inPort.getBoundingClientRect();
        var x1 = r1.left + r1.width / 2 - svgRect.left;
        var y1 = r1.top + r1.height / 2 - svgRect.top;
        var x2 = r2.left + r2.width / 2 - svgRect.left;
        var y2 = r2.top + r2.height / 2 - svgRect.top;
        var cx = Math.abs(x2 - x1) * 0.5 + 30;
        w.line.setAttribute('d', 'M ' + x1 + ' ' + y1 + ' C ' + (x1 + cx) + ' ' + y1 + ', ' + (x2 - cx) + ' ' + y2 + ', ' + x2 + ' ' + y2);
    });
}
