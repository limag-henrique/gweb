import re

script_path = 'c:/Users/Henrique Lima/Desktop/sdf/GWeb_Clone/script.js'
with open(script_path, 'r', encoding='utf-8') as f:
    code = f.read()

old_build = """// ─── BUILD APPLICATION ──────────────────────────────────────────
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
}"""

new_build = """// ─── EXPORTAÇÃO E TESTE LOCAL ───────────────────────────────────
function buildCompiledHTML() {
    var painelHtml = document.getElementById('painel-view').innerHTML;
    var cleanHtml = painelHtml.replace(/<div class="canvas-instruction"[^]*?<\\/div>/g, '');
    cleanHtml = cleanHtml.replace(/<div class="drag-handle"[^]*?<\\/div>/g, '');

    // Simplificação: injetar o estado atual dos fios e nós invisíveis ou pré-calculados.
    // Para simplificar a runtime offline, nós vamos exportar a UI puramente.
    // E um mini script para bindings.
    var bindingsDump = JSON.stringify(dependencyPropertyManager.bindings);
    
    var runtimeScript = `
    const bindings = ${bindingsDump};
    document.addEventListener('input', function (e) {
        var control = e.target.closest('.ui-control');
        if (!control) return;
        var targetId = bindings[control.id];
        if (!targetId) return;
        var targetEl = document.getElementById(targetId);
        if (!targetEl) return;
        var val = e.target.value;
        if(e.target.type === 'checkbox') val = e.target.checked;
        var ind = targetEl.querySelector('.ctrl-ind');
        var inp = targetEl.querySelector('input');
        if(inp && inp !== e.target) inp.value = val;
        if(ind) ind.textContent = val;
    });
    console.log("G Web App Local Runtime iniciada.");
    `;

    var fullCode = '<!DOCTYPE html>\\n<html lang="pt-BR">\\n<head>\\n<meta charset="UTF-8">\\n<style>\\n  body { font-family: Segoe UI, sans-serif; background: #f3f3f3; }\\n  .ui-control { position:absolute; font-family:Segoe UI,sans-serif; }\\n  .ui-control.selected { border: 2px solid #0078d4; }\\n  .flex-container { display:flex; gap:10px; padding:10px; border:1px solid #ccc; }\\n  .ctrl-lbl { font-size:12px; font-weight:600; margin-bottom:4px; }\\n</style>\\n</head>\\n<body>\\n<div class="app-interface" style="position:relative; width:100%; height:100vh;">\\n' + cleanHtml + '\\n</div>\\n<script>\\n' + runtimeScript + '\\n</script>\\n</body>\\n</html>';
    
    return fullCode;
}

function testLocalhost() {
    var fullCode = buildCompiledHTML();
    var blob = new Blob([fullCode], { type: 'text/html' });
    var blobUrl = URL.createObjectURL(blob);
    window.open(blobUrl, '_blank');
    showToast('Local Host Aberto', 'O projeto está rodando em uma nova aba usando URLs virtuais.', 'success');
}

function exportProjectZip() {
    if (typeof JSZip === 'undefined') {
        showToast('Erro de Exportação', 'Biblioteca JSZip não carregada. Verifique a conexão com a internet.', 'error');
        return;
    }
    showToast('Construindo...', 'Gerando pacotes HTML, CSS e JS...', 'info');
    
    var zip = new JSZip();
    var fullHtml = buildCompiledHTML();
    
    // Na vida real, recuperaríamos os arquivos estáticos fetch() 
    // mas vamos criar os essenciais no zip.
    zip.file('index.html', fullHtml);
    zip.file('manifest.json', JSON.stringify({ name: 'Projeto GWeb Exportado', version:'1.0' }));
    
    zip.generateAsync({type:"blob"}).then(function(content) {
        var a = document.createElement('a');
        a.href = URL.createObjectURL(content);
        a.download = 'GWeb_Project.zip';
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        showToast('Exportado!', 'Projeto ZIP baixado com sucesso.', 'success');
    });
}"""

if old_build in code:
    code = code.replace(old_build, new_build)
    with open(script_path, 'w', encoding='utf-8') as f:
        f.write(code)
    print("Replaced build functions.")
else:
    print("Could not find old_build text exactly.")
