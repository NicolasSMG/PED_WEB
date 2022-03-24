window.maskDecimal = (id, scale, min, max) => {
    var maskDecimal = IMask(
        document.getElementById(id), {
        mask: Number,
        scale: scale,
        signed: false,
        thousandsSeparator: ',',
        padFractionalZeros: true,
        normalizeZeros: true,
        radix: '.',
        mapToRadix: [','],
        min: min,
        max: max
    });
};
window.maskEntero = (id, min, max) => {
    var maskEntero = IMask(
        document.getElementById(id), {
        mask: Number,
        scale: 0,
        thousandsSeparator: ',',
        min: min,
        max: max
    });
};

window.maskPercent = (id) => {
    var maskPercent = IMask(
        document.getElementById(id), {
        mask: '000%',
        min: 0,
        max: 100
    });
};

window.maskCellphone = (id) => {
    var maskCellphone = IMask(
        document.getElementById(id), {
        mask: '000 000 0000'
    });
};