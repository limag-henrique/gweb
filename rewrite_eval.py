import re

script_path = 'c:/Users/Henrique Lima/Desktop/sdf/GWeb_Clone/script.js'
with open(script_path, 'r', encoding='utf-8') as f:
    code = f.read()

# 1. Fix the evaluation loop to support outIdx
old_wire_route = """        // Route outputs → inputs
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
        });"""

new_wire_route = """        // Route outputs → inputs
        wiringState.wires.forEach(function (w) {
            if (vals[w.outBox] && vals[w.inBox]) {
                var outKey = 'out' + w.outIdx;
                var v = vals[w.outBox][outKey] !== undefined ? vals[w.outBox][outKey] : vals[w.outBox].out;
                if (v !== null && v !== undefined) {
                    var key = 'in' + w.inIdx;
                    if (vals[w.inBox][key] !== v) {
                        vals[w.inBox][key] = v;
                        changed = true;
                    }
                }
            }
        });"""

code = code.replace(old_wire_route, new_wire_route)

# 2. Add support for multiple inputs initialization
init_old = """    // 1. Initialize all nodes
    allNodes.forEach(function (node) {
        var t = node.getAttribute('data-type');
        vals[node.id] = { in0: null, in1: null, out: null };"""

init_new = """    // 1. Initialize all nodes
    allNodes.forEach(function (node) {
        var t = node.getAttribute('data-type');
        vals[node.id] = { in0: null, in1: null, in2: null, out: null, out0: null, out1: null };"""
code = code.replace(init_old, init_new)


# 3. Add all the logic nodes evaluation
logic_old = """            if (t === 'add_node') v.out = a + b;
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

            if (v.out !== prev) changed = true;"""

logic_new = """            var c = v.in2 !== null ? v.in2 : 0; // 3rd input support

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
            
            // Advanced Math
            else if (t === 'quotient_rem') { v.out0 = b !== 0 ? Math.floor(a/b) : 0; v.out1 = b !== 0 ? a % b : 0; v.out = v.out0; }
            else if (t === 'scale_pow2_node') v.out = a * Math.pow(2, b);
            else if (t === 'max_min_node') { v.out0 = Math.max(a,b); v.out1 = Math.min(a,b); v.out = v.out0; }
            else if (t === 'inc_node') v.out = a + 1;
            else if (t === 'dec_node') v.out = a - 1;
            else if (t === 'round_near_node') v.out = Math.round(a);
            else if (t === 'round_ninf_node') v.out = Math.floor(a);
            else if (t === 'round_pinf_node') v.out = Math.ceil(a);
            else if (t === 'square_node') v.out = a * a;
            else if (t === 'neg_node') v.out = -a;
            else if (t === 'recip_node') v.out = a !== 0 ? 1/a : 0;
            else if (t === 'sign_node') v.out = Math.sign(a);
            else if (t === 'in_range_node') v.out = (a >= b && a <= c) ? 1 : 0;
            
            // Array sums and Muls (simplification assuming single input loops / scalars for UI)
            else if (t === 'sum_array' || t === 'mul_array') v.out = a; // Fallback, real arrays need data struct
            
            // Advanced Logic
            else if (t === 'xor_node') v.out = (!!a !== !!b) ? 1 : 0;
            else if (t === 'nand_node') v.out = !(a && b) ? 1 : 0;
            else if (t === 'nor_node') v.out = !(a || b) ? 1 : 0;
            else if (t === 'nxor_node') v.out = (!!a === !!b) ? 1 : 0;
            else if (t === 'implies_node') v.out = (!a || b) ? 1 : 0;
            else if (t === 'bool_to_int') v.out = a ? 1 : 0;
            else if (t === 'and_array' || t === 'or_array' || t === 'num_to_bool_array' || t === 'bool_array_to_num') v.out = a;

            // Trigonometry
            else if (t === 'sin_node') v.out = Math.sin(a);
            else if (t === 'cos_node') v.out = Math.cos(a);
            else if (t === 'tan_node') v.out = Math.tan(a);
            else if (t === 'csc_node') v.out = Math.sin(a) !== 0 ? 1/Math.sin(a) : 0;
            else if (t === 'sec_node') v.out = Math.cos(a) !== 0 ? 1/Math.cos(a) : 0;
            else if (t === 'cot_node') v.out = Math.tan(a) !== 0 ? 1/Math.tan(a) : 0;
            else if (t === 'asin_node') v.out = Math.asin(a);
            else if (t === 'acos_node') v.out = Math.acos(a);
            else if (t === 'atan_node') v.out = Math.atan(a);
            else if (t === 'atan2_node') v.out = Math.atan2(a, b);
            else if (t === 'sincos_node') { v.out0 = Math.sin(a); v.out1 = Math.cos(a); v.out = v.out0; }
            else if (t === 'sinc_node') v.out = a !== 0 ? Math.sin(a)/a : 1;

            if (v.out !== prev || (t === 'quotient_rem' || t === 'max_min_node' || t === 'sincos_node')) changed = true;"""

code = code.replace(logic_old, logic_new)

with open(script_path, 'w', encoding='utf-8') as f:
    f.write(code)

print("evaluateDataflow patched.")
