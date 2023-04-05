  var appPort = window.location.port;
  
  // zobrazi chybovu hlasku vo vybranom elemente  
  function showAlertError(message) {

      $("#alert_error").show()
      $("#alert-text").html(message)
      $("#alert_error").fadeTo(2000, 500).slideUp(500, function () {
        $("#alert_error").slideUp(500);
      });
    }
// zobrazi hlasenie o uspechu vo vybranom elemente 
    function showAlertSucces(message) {

      $("#alert_success").show()
      $("#alert-text").html(message)
      $("#alert_success").fadeTo(2000, 500).slideUp(500, function () {
        $("#alert_success").slideUp(500);
      });
    }
function showAlert(message, Id, type) {
    var alertContainer = "#" + Id + " #alert_container";
    var alertText = "#" + Id + " #alert-text";
    var alertClass = ".alert alert-" + type;

    $(alertContainer).show();
    $(alertText).html(message);
    $(alertContainer).addClass(alertClass);
    $(alertContainer).fadeTo(2000, 500).slideUp(500, function () {
      $(alertContainer).removeClass(alertClass);
      $(alertContainer).slideUp(500);
    });
}

// odosle ajaxovy request na webovú službu  cesta na ktoru sa nachadzadza v  parametri url s parametrom a datami podľa dynamicky vygenerovaneho formulara na zaklade uživatelového výberu. Pri uspechu vloži html do vybraných html elementov a posunie pohlad uzivatela na card s knihou
    function ajaxSingleBookForm(formid, url, parameter, parameterdata) {
      $(document).on('submit', formid, function (event) {
        console.log(parameterdata)
        var formData = {};
        formData[parameter] = $("#" + parameterdata).val().toLowerCase();
        console.log(formData)
        $.ajax({
          url: url,
          method: "POST",
          data: formData,
          dataType: "json",
          encode: true,
        }).done(function (data) {
          $(".card-body").show();
          $('html, body').animate({
    scrollTop: $("#nazov_hore").offset().top
  }, 1200);
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
          $("#vazba").html(data.book.vazba)
          $("#isbn").html(data.book.isbn)
          $("#jazyk").html(data.book.jazyk)
          $("#pocet_stran").html(data.book.pocet_stran)
          $("#rok_vydania").html(data.book.rok_vydania)
          $("#vydavatelstvo").html(data.book.vydavatelstvo)
          $("#predajna_cena").html(data.book.predajna_cena)
          $("#nakupna_cena").html(data.book.nakupna_cena)
          $("#marza").html(parseFloat(data.book.marza).toFixed(2) * 100 + "%")
          $("#zisk_kus").html(data.book.zisk_kus)
          $("form")[0].reset();
         
        }).fail(function (jqXHR, textStatus, errorThrown) { showAlert(jqXHR.responseText, "alerts", "danger") });
        event.preventDefault();
      })
    }
// asynchronne zavola službu ktora vrati všetky knihy asychnronne je kvoli tomu ze musime pockat kym server vrati hodnoty az potom mozme v casti done vratit data ak by to nebolo asychronne tak return by vratil udefined 
     async function Getallbook() {
      // await zabezpeci sychronny priebeh a caka kym nedostane promise 
      const result = await $.ajax({
        url: "http://localhost:" + appPort + "/book_services.asmx/GetListAllBooks",
        type: "POST",
        dataType: 'json',
      }).done(function (data) { })

      return result.books.book
    }

     // naplni vybrane pole udajom o jednom atribute vsetkých knih 
  async function createArrayFromOneAttribute(attribute, array) {

   await Getallbook().then((data) => {

      for (values of data) {
        array.push(values[attribute])
      }
      
    })
  }
   async function createArrayFromOneAttribute1(attribute) {
let array=[]
   await Getallbook().then((data) => {
     
      for (values of data) {
        array.push(values[attribute])
      }
     
    })
     const uniqueArray = [...new Set(array)];
    return uniqueArray
  }
  
  async function createArrayFromNestedAttribute(parent,child) {
 let array=[]
   await Getallbook().then((data) => {
     
     for (values of data) {
        array.push(values[parent][child])
      }
     })
     const uniqueArray = [...new Set(array)];
     return uniqueArray
   }
  // do zadaneho inputu prida autonávrhy data ziska z poľa ktoré vytvorí napr. funkcia ceateArrayFromOneAttribute  
  function autoCompleteInput(inputSelector, maxItem, sourceArray) {
    var selector = inputSelector;
    $(document).on("click", selector, function () {
      $(this).autocomplete({
        maxShowItems: maxItem, // Make list height fit to 5 items when items are over 5.
        source: sourceArray
      });
    });
  }
  // potreba zafixovat footer pri niektorych castiach kodu 
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


  // Nastavenie výšky všetkých kariet na výšku najvyššej karty všetkých riadkov
  function calculateCardBody(){
  
    var tallestCard = 0; // Premenná pre najvyššiu kartu

    // Prechádza všetky riadky
    $('.row').each(function () {
      // Prechádza všetky karty v danom riadku
      $(this).find('.card').each(function () {
        // Ak je výška tejto karty väčšia ako doteraz najvyššia karta
        if ($(this).height() > tallestCard) {
          tallestCard = $(this).height(); // Nastaví novú najvyššiu kartu
        }
      });
    });
    // Nastaví výšku všetkých kariet na výšku najvyššej karty
    $('.card').height(tallestCard);
  };


 
