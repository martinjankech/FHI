  function showAlertError(message) {

      $("#alert_error").show()
      $("#alert-text").html(message)
      $("#alert_error").fadeTo(2000, 500).slideUp(500, function () {
        $("#alert_error").slideUp(500);
      });
    }

    function showAlertSucces(message) {

      $("#alert_success").show()
      $("#alert-text").html("message")
      $("#alert_success").fadeTo(2000, 500).slideUp(500, function () {
        $("#alert_success").slideUp(500);
      });
    }

    function ajaxSingleBookForm(formid, url, parameter, parameterdata) {
      $(document).on('submit', formid, function (event) {
        console.log(parameterdata)
        var formData = {};
        formData[parameter] = $("#" + parameterdata).val().toLowerCase();

        $.ajax({
          url: url,
          method: "POST",
          data: formData,
          dataType: "json",
          encode: true,
          // 

        }).done(function (data) {

          $(".card-body").show();
          $('footer').css({
            'position': ' relative',
            ' width': '100 %',
            'bottom': '1vh',
          })


          if (data.book.autori.autor2 != "-") {
            $("#autori").html(data.book.autori.autor1 + "," + data.book.autori.autor2)
            $("#autori_hore").html(data.book.autori.autor1 + "," + data.book.autori.autor2)
          }
          else {
            $("#autori").html(data.book.autori.autor1)
            $("#autori_hore").html(data.book.autori.autor1)
          }
          $("#obrazok").attr('src', data.book.obrazok)
          $("#obsah").html(data.book.obsah)
          $("#cena_hl").html("€" + data.book.predajna_cena)
          $("#nazov_hore").html(data.book.nazov)
          $("#nazov").html(data.book.nazov)
          $("#kategoria").html(data.book.kategoria)
          $("#isbn").html(data.book.isbn)
          $("#jazyk").html(data.book.jazyk)
          $("#pocet_stran").html(data.book.pocet_stran)
          $("#rok_vydania").html(data.book.rok_vydania)
          $("#vydavatelstvo").html(data.book.vydavatelstvo)
          $("#predajna_cena").html(data.book.predajna_cena)
          $("#nakupna_cena").html(data.book.nakupna_cena)
          $("#marza").html(parseFloat(data.book.marza).toFixed(2) * 100 + "%")
          $("#zisk_kus").html(data.book.zisk_kus)
        }).fail(function (jqXHR, textStatus, errorThrown) { showAlertError(errorThrown) });
        event.preventDefault();
      })
    }

     async function Getallbook() {
      // await zabezpeci sychronny priebeh a caka kym nedostane promise 
      const result = await $.ajax({
        url: 'http://localhost:8045/book_services.asmx/GetListAllBooks',
        type: "POST",
        dataType: 'json',
      }).done(function (data) { })

      return result.books.book
    }

     // naplni vybrane pole udajom o jednom atribute vsetkzch knih 
  function createArrayFromOneAttribute(attribute, array) {

    Getallbook().then((data) => {
      for (values of data) {
        array.push(values[attribute])
      }
    })
  }
  // do zadaneho inputu prida autonavhy 
  function autoCompleteInput(inputSelector, maxItem, sourceArray) {
    var selector = inputSelector;
    $(document).on("click", selector, function () {
      $(this).autocomplete({
        maxShowItems: maxItem, // Make list height fit to 5 items when items are over 5.
        source: sourceArray
      });
    });
  }
function fixedFooter(){
  jQuery(function ($) {
    $(document).ready(function () {
      if ($('body').height() < $(window).height()) {
        $('footer').css({
          'position': 'fixed',
          'bottom': '12vh',
          'left': '0',
          'right': '0'
        });
      }
    });
  });
 }

 function displayHash() {
  var theHash = window.location.hash;
  if (theHash.length == 0) { theHash = "_index"; }
  var elems = document.querySelectorAll("#caption");
  elems[0].innerText = "Current Hash: " + theHash;
  return true;
}
  
