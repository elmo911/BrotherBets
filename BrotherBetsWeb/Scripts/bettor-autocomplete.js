$(document).ready(function () {
    $("#BettorAutocomplete").autocomplete({
        source: "/BettorApi/BettorNameAutoComplete" 
    });
})  