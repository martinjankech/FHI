
function includeHTML() {
  var z, i, elmnt, file, xhttp;
  /* Loop through a collection of all HTML elements: */
  z = document.getElementsByTagName("*");
  for (i = 0; i < z.length; i++) {
    elmnt = z[i];
    /*search for elements with a certain atrribute:*/
    file = elmnt.getAttribute("w3-include-html");
    if (file) {
      /* Make an HTTP request using the attribute value as the file name: */
      xhttp = new XMLHttpRequest();
      xhttp.onreadystatechange = function() {
        if (this.readyState == 4) {
          if (this.status == 200) {elmnt.innerHTML = this.responseText;}
          if (this.status == 404) {elmnt.innerHTML = "Page not found.";}
          /* Remove the attribute, and call this function once more: */
          elmnt.removeAttribute("w3-include-html");
          includeHTML();
        }
      }
      xhttp.open("GET", file, true);
      xhttp.send();
      /* Exit the function: */
      return;
    }
  }
}

function ajaxSingleBook(elementid,url, method = 'POST', data)
      $(elementid).submit(function (event) {
            $.ajax({
                url: url,
                method: method,
                data: data,        
                encode: true,
            }).done(function (data) {
          if (data.book.autori.autor2 != undefined) {
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
        })
       event.preventDefault();
      })
      
              
         
    
        
  