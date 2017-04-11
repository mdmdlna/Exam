function parseDate(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);
        if (results && results.length > 3)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));

        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);

        if (results && results.length > 6)

            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]));

        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);

        if (results && results.length > 7)

            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]), parseInt(results[7]));

    }
    return null;
}


function getQueryParams(params) {
    var query = {
        limit: params.limit,
        offset: params.offset,
        order: params.order,
        ordername: params.sort,
        filters: getSerachJson()
    };
    return query;
}

function getSerachJson() {
    var searchParames = $('#searchParams').serializeArray();      // jQuery推荐方式
    $.each(searchParames, function (index, data) {
        if (data != undefined) {
            data.operType = $('[name="' + data.name + '"]', $('#searchParams')).data("operate-type");
            if (data.operType == "Between") {
                var inputId1 = $('[name="' + data.name + '"]', $('#searchParams')).data("bind-datemin");
                var inputId2 = $('[name="' + data.name + '"]', $('#searchParams')).data("bind-datemax");
                var minVal = $("#" + inputId1).val();
                var maxVal = $("#" + inputId2).val();

                if (minVal == undefined || minVal == "") {
                    minVal = "1900-01-01 00:00:00";
                } else {
                    minVal += " 00:00:00";
                }
                if (maxVal == undefined || maxVal == "") {
                    maxVal = "2100-12-31 23:59:59";
                } else {
                    maxVal += " 23:59:59";
                }

                data.value = minVal + "," + maxVal;
            }
        }
    });
    var jsonFilters = JSON.stringify(searchParames);
    return jsonFilters;
}


//function PriceFormatter(value, row, index) {
//    if (/[^0-9\.]/.test(row.TotalPrice)) {
//        return "面议";
//    }
//    var s = row.TotalPrice.toString();
//    s = s.replace(/^(\d*)$/, "$1.");
//    s = (s + "00").replace(/(\d*\.\d\d)\d*/, "$1");
//    s = s.replace(".", ",");
//    var re = /(\d)(\d{3},)/;
//    while (re.test(s)) {
//        s = s.replace(re, "$1,$2");
//    }
//    s = s.replace(/,(\d\d)$/, ".$1");
//    return s.replace(/^\./, "0.");
//}


function TimeFormatter(value, row, index) {
    if (typeof value == 'string') value = parseDate(value);
    if (value instanceof Date) {
        var y = value.getFullYear();
        var m = value.getMonth() + 1;
        var d = value.getDate();
        value = y + '-' + m + '-' + d;
        return value;
    }
    return "";
}