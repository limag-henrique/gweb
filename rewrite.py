import re

with open('c:/Users/Henrique Lima/Desktop/sdf/GWeb_Clone/script.js', 'r', encoding='utf-8') as f:
    code = f.read()

# Replace DIAGRAM_TYPES
diagram_types_pattern = r'var DIAGRAM_TYPES = \[[^\]]+\];'
new_diagram_types = """var DIAGRAM_TYPES = [
    'while_loop', 'for_loop', 'case_struct', 'event_struct', 'wait_ms', 'tick_count', 'wait_until', 'comment',
    'add_node', 'sub_node', 'mul_node', 'div_node', 'quotient_rem', 'inc_node', 'dec_node', 'sum_array', 'mul_array', 'abs_node', 'round_near_node', 'round_ninf_node', 'round_pinf_node', 'scale_pow2_node', 'sqrt_node', 'square_node', 'neg_node', 'recip_node', 'sign_node', 'max_min_node', 'in_range_node', 'rand_num', 'num_const', 'enum_const',
    'sin_node', 'cos_node', 'tan_node', 'csc_node', 'sec_node', 'cot_node', 'asin_node', 'acos_node', 'atan_node', 'atan2_node', 'sincos_node', 'sinc_node',
    'gt_node', 'lt_node', 'eq_node', 'not_node', 'and_node', 'or_node', 'xor_node', 'nand_node', 'nor_node', 'nxor_node', 'implies_node', 'and_array', 'or_array', 'bool_to_int', 'num_to_bool_array', 'bool_array_to_num', 'bool_const',
    'str_const', 'str_concat', 'num2str_node', 'str2num_node',
    'obt_queue', 'enq_elem', 'deq_elem', 'ui_read', 'ui_write', 'local_var_read', 'local_var_write'
];"""
code = re.sub(diagram_types_pattern, new_diagram_types, code, count=1)

# Find end of htmlGenerators
# It ends around 'local_var_write'
end_marker = "    'local_var_write': function () { return '<div class=\"g-node g-node-wide\" style=\"background:#d5f5e3;border-color:#27ae60;font-size:10px;\"><div class=\"g-port\" data-io=\"in\" data-idx=\"0\"></div><i class=\"fa-solid fa-upload\" style=\"margin-right:2px;font-size:9px;\"></i>WriteVar</div>'; }"

new_html_generators = """    'divider':     function () { return '<hr style="width:100%;border:1px solid #ccc;margin:10px 0;pointer-events:none;">'; },
    'image_dec':   function () { return '<div style="width:100px;height:100px;background:#eee;border:1px dashed #999;display:flex;align-items:center;justify-content:center;color:#999;font-size:11px;"><i class="fa-regular fa-image" style="font-size:24px;"></i></div>'; },
    'radio_btn':   function () { return '<label style="cursor:pointer;display:flex;align-items:center;font-size:12px;font-weight:600;"><input type="radio" name="grp_1" style="margin-right:5px;cursor:pointer;"> Seleção</label>'; },
    'color_led':   function () { return '<div class="ctrl-lbl">Color LED</div><div class="ctrl-led" onclick="this.style.background=this.style.background===\\'red\\'?\\'blue\\':(this.style.background===\\'blue\\'?\\'yellow\\':\\'red\\')" style="cursor:pointer;background:red;"></div>'; },
    'spreadsheet': function () { return '<div class="ctrl-lbl">Spreadsheet</div><textarea readonly style="width:200px;height:80px;font-family:monospace;font-size:11px;resize:both;">A1,B1\\nA2,B2</textarea>'; },
    'tree_view':   function () { return '<div class="ctrl-lbl">Tree View</div><ul style="border:1px solid #ccc;padding:5px 5px 5px 20px;font-size:12px;background:white;width:150px;min-height:50px;"><li>Node 1<ul><li>Child 1</li></ul></li><li>Node 2</li></ul>'; },
    
    'wait_ms':     function () { return '<div class="g-node g-node-sys g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><i class="fa-solid fa-hourglass-half" style="margin-right:3px;"></i> Wait MS</div>'; },
    'tick_count':  function () { return '<div class="g-node g-node-sys g-node-wide"><div class="g-port" data-io="out" data-idx="0"></div><i class="fa-solid fa-stopwatch" style="margin-right:3px;"></i> ms Tick</div>'; },
    'wait_until':  function () { return '<div class="g-node g-node-sys g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><i class="fa-solid fa-clock" style="margin-right:3px;"></i> Wait Until</div>'; },
    'comment':     function () { return '<div style="background:transparent;border:none;color:#107c10;font-family:Consolas, monospace;font-size:12px;padding:5px;">// Comentário...</div>'; },

    'quotient_rem':function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div><div class="g-port" data-io="out" data-idx="1" style="top:25px;"></div>x÷y</div>'; },
    'scale_pow2_node':function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>x*2^n</div>'; },
    'max_min_node':function () { return '<div class="g-node"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div><div class="g-port" data-io="out" data-idx="1" style="top:25px;"></div>M/m</div>'; },
    'inc_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>x+1</div>'; },
    'dec_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>x-1</div>'; },
    'sum_array':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>Σ [x]</div>'; },
    'mul_array':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>Π [x]</div>'; },
    'round_near_node':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>~x</div>'; },
    'round_ninf_node':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>⌊x⌋</div>'; },
    'round_pinf_node':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>⌈x⌉</div>'; },
    'square_node': function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>x²</div>'; },
    'neg_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>-x</div>'; },
    'recip_node':  function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>1/x</div>'; },
    'sign_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>sgn(x)</div>'; },
    'in_range_node':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0" style="top:5px;"></div><div class="g-port" data-io="in" data-idx="1" style="top:15px;"></div><div class="g-port" data-io="in" data-idx="2" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>In Range</div>'; },
    'enum_const':  function () { return '<div class="g-node g-node-wide" style="background:#d4edda;border-color:#28a745;font-size:12px;"><div class="g-port" data-io="out" data-idx="0"></div><span class="const-display" style="font-size:10px;">Enm</span></div>'; },

    'xor_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>XOR</div>'; },
    'nand_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>NAND</div>'; },
    'nor_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>NOR</div>'; },
    'nxor_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>NXOR</div>'; },
    'implies_node':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>x=>y</div>'; },
    'and_array':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>AND[x]</div>'; },
    'or_array':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>OR[x]</div>'; },
    'bool_to_int': function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>B→I</div>'; },
    'num_to_bool_array':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>N→[B]</div>'; },
    'bool_array_to_num':function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>[B]→N</div>'; },
    
    'sin_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>sin</div>'; },
    'cos_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>cos</div>'; },
    'tan_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>tan</div>'; },
    'csc_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>csc</div>'; },
    'sec_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>sec</div>'; },
    'cot_node':    function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>cot</div>'; },
    'asin_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>asin</div>'; },
    'acos_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>acos</div>'; },
    'atan_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>atan</div>'; },
    'atan2_node':  function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="in" data-idx="1" style="top:25px;"></div><div class="g-port" data-io="out" data-idx="0"></div>atan2</div>'; },
    'sincos_node': function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0" style="top:5px;"></div><div class="g-port" data-io="out" data-idx="1" style="top:25px;"></div>sin+cos</div>'; },
    'sinc_node':   function () { return '<div class="g-node g-node-wide"><div class="g-port" data-io="in" data-idx="0"></div><div class="g-port" data-io="out" data-idx="0"></div>sinc</div>'; },
"""
code = code.replace(end_marker, end_marker + "\n" + new_html_generators)

with open('c:/Users/Henrique Lima/Desktop/sdf/GWeb_Clone/script.js', 'w', encoding='utf-8') as f:
    f.write(code)
print("Finished updates to generators.")
