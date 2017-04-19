$(document).ready(function () {
    $("#BettorAutocomplete").autocomplete({
        source: "https://bro.wolfe.solutions/bets/BettorApi/BettorNameAutoComplete" 
    });
})  