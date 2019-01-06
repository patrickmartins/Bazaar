(function () {
    "use strict";

    angular.module("bazaar-app")
        .filter("prettyDate", prettydate_filter);

    prettydate_filter.$inject = ["$window"];

    function prettydate_filter() {
        return function (span, type) {
            var date = new Date(span);

            if (type === "elapsed") {
                var diff = (((new Date()).getTime() - date.getTime()) / 1000),
                    day_diff = Math.floor(diff / 86400);

                if (isNaN(day_diff) || day_diff < 0)
                    return;

                return day_diff === 0 && (
                    diff < 60 && "Agora mesmo" ||
                    diff < 120 && "1 minuto atrás" ||
                    diff < 3600 && Math.floor(diff / 60) + " minutos atrás" ||
                    diff < 7200 && "1 hora atrás" ||
                    diff < 86400 && Math.floor(diff / 3600) + " horas atrás") ||
                    day_diff === 1 && "Ontem" ||
                    day_diff < 7 && day_diff + " dias atrás" ||
                    day_diff < 31 && Math.floor(day_diff / 7) + " semanas atrás" ||
                    day_diff < 63 && "1 mês atrás" ||
                    (day_diff > 31 && day_diff < 360) && Math.floor(day_diff / 30) + " meses atrás" ||
                    day_diff < 430 && "1 ano atrás" ||
                    day_diff > 365 && Math.floor(day_diff / 365) + " anos atrás";

            } else if (type === "short") {
                return ("0" + date.getDate()).slice(-2) + "/" + ("0" + (date.getMonth() + 1)).slice(-2) + "/" + ("0" + date.getFullYear()).slice(-2);

            } else if (type === "full") {
                var days = ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"];
                var months = ["janeiro", "fevereiro", "março", "abril", "maio", "junho", "julho", "agosto", "setembro", "outubro", "novembro", "dezembro"];

                return days[date.getDay()] + ", " +
                    date.getDate() + " de " +
                    months[date.getMonth()] + " de " +
                    date.getFullYear() + " às " +
                    ("0" + date.getHours()).slice(-2) + ":" + ("0" + date.getMinutes()).slice(-2);
            }

            return "";
        }
    }

})();