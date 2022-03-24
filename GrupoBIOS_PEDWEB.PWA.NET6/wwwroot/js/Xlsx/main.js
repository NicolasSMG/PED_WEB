
function XlmxToJson(json) {
    var workbook;
    var exceljson = '{ ';
    workbook = XLSX.read(json, { type: 'base64' });
    var sheet_name_list = workbook.SheetNames;
    var contador = 0
    sheet_name_list.forEach(function (titulo) {
        /*Convierte las celdas en JSON*/
        var contenido = JSON.stringify(XLSX.utils.sheet_to_json(workbook.Sheets[titulo]));
        exceljson += '"' + titulo + '" : ' + contenido + ' ';
        if (contador != sheet_name_list.length - 1) {
            exceljson += ',';
        }
        else {
            exceljson += '}';
        }
        contador++;
    });
    if (exceljson.length > 0) {
        return exceljson;
    }
}