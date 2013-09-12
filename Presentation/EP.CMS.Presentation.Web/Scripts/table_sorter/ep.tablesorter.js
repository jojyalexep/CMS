$(document).ready(function (e) {
    $(".adminlist")
   .tablesorter({
       widthFixed: true, widgets: ['zebra'],
       textExtraction: function (node) {
           // extract data from markup and return it  
           return $(node).text();;
       }


   })
   .tablesorterPager({ container: $(".pagination") });
});