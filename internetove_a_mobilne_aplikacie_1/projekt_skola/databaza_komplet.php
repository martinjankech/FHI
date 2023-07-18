<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <link rel="stylesheet" href="style.css" />
  <title>Tabuky</title>
  <style>
    table,
    th,
    td {
      border: solid black;
      width: 600px;
      background-color: lightsalmon;
      font-size: 15px;
      text-align: center
    }
  </style>

</head>

<body>
  <div id="page-container">
    <div id="content-wrap">
      <div class="header">
        <h1>Happy Burger</h1>
      </div>

      <nav class="navigacia">
        <a href="index.html">Domov</a>
        <a href="produkty.html">Produkty</a>
        <a href="kontakt.html">Kontakt</a>
        <a href="zadania.html">Zadania</a>
        <a href="databaza_komplet.php">Databáza</a>
        <a href="vyhladavanie.php">Vyhladavanie</a>
      </nav>

      <img src="./images/burgre_tacka.jfif" class="tacka_logo" alt="burger tacka logo" />
      <a href="index.html"><img src="./images/happy-burger-logo_zmensene.jpg" class="logo" alt="logo" /></a>

      <br />
      <br />
      <center>
        <H1>Databáza podniku Happy Burger</H1>

        <form action="databaza_komplet.php#ostan" id="ostan" method="POST">
          Vyberte tabulku: <select name="tabulka">
            <option value="vyrobok">vyrobok</option>
            <option value="material">material</option>
            <option value="vyroba">vyroba</option>
            <option value="dodany_vyrobok">dodany_vyrobok</option>
            <option value="Dodaci_list">Dodaci_list</option>
            <option value="Dodavatel">Dodavateľ</option>
            <option value="kraj">kraj</option>
            <option value="Mesto">Mesto</option>
            <option value="Objednany_mat">Objednany_mat</option>
            <option value="Objednavka_mat">Objednavka_mat</option>
            <option value="Odberatel">Odberatel</option>
            <option value="Okres">Okres</option>
            <option value="skupina_mat">skupina_mat</option>
            <option value="skupina_vyr">skupina_vyr</option>
            <option value="tabulka_faktov1">tabulka_faktov1</option>
            <option value="tabulka_faktov2">tabulka_faktov2</option>
            <option value="tabulka_faktov3">tabulka_faktov3</option>
            <option value="typ_mat">typ_mat</option>
            <option value="Typ_vyrobku">Typ_vyrobku</option>


          </select>
          <input class="submit" type="submit" class="btn" name="search" value="potvrdiť"><br>

          <?php

          /*$connection = mysqli_connect("localhost", "root", "");
          $db = mysqli_select_db($connection, 'jankechwzcz7860');
          */
          $servername = "sql2.webzdarma.cz";
          $username = "jankechwzcz7860";
          $password = "Martin89";
          $dbname = "jankechwzcz7860";
          // Create connection
          $connection = mysqli_connect($servername, $username, $password, $dbname);
          // Check connection
          if (!$connection) {
            die("Connection failed: " . mysqli_connect_error());
          }


          if (!empty($_POST['tabulka']))

            switch ($_POST['tabulka']) {
              case 'vyrobok':
          ?>
              <table>
                <tr>
                  <th>ID_vyr</th>
                  <th>nazov_vyrobku</th>
                  <th>typ_vyr</th>
                  <th>vyr_cena</th>
                  <th>pred_cena</th>
                </tr>

                <?php

                $query = "SELECT *FROM `vyrobok`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_vyr'] ?> </td>
                    <td> <?php echo $row['nazov_vyrobku'] ?> </td>
                    <td> <?php echo $row['typ_vyr'] ?> </td>
                    <td> <?php echo $row['vyr_cena'] ?> </td>
                    <td> <?php echo $row['pred_cena'] ?> </td>


                  <?php

                }
                  ?>
              </table>
            <?php

                break;
              case 'material':
            ?>
              <table>
                <tr>
                  <th>ID_mat</th>
                  <th>nazov_materialu</th>
                  <th>typ_mat</th>
                  <th>mern_jednotka</th>
                  <th>jednotkova_cena</th>
                </tr>
                <?php

                $query = "SELECT *FROM `material`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_mat'] ?> </td>
                    <td> <?php echo $row['nazov'] ?> </td>
                    <td> <?php echo $row['typ_mat'] ?> </td>
                    <td> <?php echo $row['mern_jednotka'] ?> </td>
                    <td> <?php echo $row['jedn_cena'] ?> </td>
                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'vyroba':
            ?>
              <table>
                <tr>
                  <th>ID_vyr</th>
                  <th>ID_mat</th>
                  <th>mnozstvo_mat</th>

                </tr>
                <?php

                $query = "SELECT *FROM `vyroba`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_vyr'] ?> </td>
                    <td> <?php echo $row['ID_mat'] ?> </td>
                    <td> <?php echo $row['mnozstvo_materialu'] ?> </td>

                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'Dodaci_list':
            ?>
              <table>
                <tr>
                  <th>ID_obj</th>
                  <th>dat_obj</th>
                  <th>datum_dod</th>
                  <th>ID_odoberatela</th>
                  <th>cena</th>

                </tr>
                <?php

                $query = "SELECT *FROM `dodaci_list`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_obj'] ?> </td>
                    <td> <?php echo $row['dat_obj'] ?> </td>
                    <td> <?php echo $row['datum_dod'] ?> </td>
                    <td> <?php echo $row['ID_odoberatela'] ?> </td>
                    <td> <?php echo $row['cena'] ?> </td>

                  <?php
                }
                  ?>
              </table>
            <?php
                break;
              case 'dodany_vyrobok':
            ?>
              <table>
                <tr>
                  <th>ID_obj</th>
                  <th>ID_vyr</th>
                  <th>mnozstvo</th>


                </tr>
                <?php

                $query = "SELECT *FROM `dodany_vyrobok`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_obj'] ?> </td>
                    <td> <?php echo $row['ID_vyr'] ?> </td>
                    <td> <?php echo $row['mnozstvo'] ?> </td>




                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'Dodavatel':
            ?>
              <table>
                <tr>
                  <th>ID_dodavatela</th>
                  <th>naz_doda</th>
                  <th>ico</th>
                  <th>mesto</th>
                  <th>ulica</th>
                  <th>c_domu</th>
                  <th>psc</th>
                  <th>tel_c</th>
                  <th>email</th>
                  <th>smer</th>
                </tr>
                <?php

                $query = "SELECT *FROM `dodavatel`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_dodavatela'] ?> </td>
                    <td> <?php echo $row['naz_doda'] ?> </td>
                    <td> <?php echo $row['ico'] ?> </td>
                    <td> <?php echo $row['mesto'] ?> </td>
                    <td> <?php echo $row['ulica'] ?> </td>
                    <td> <?php echo $row['c_domu'] ?> </td>
                    <td> <?php echo $row['psc'] ?> </td>
                    <td> <?php echo $row['tel_c'] ?> </td>
                    <td> <?php echo $row['email'] ?> </td>
                    <td> <?php echo $row['smer'] ?> </td>

                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'kraj':
            ?>
              <table>
                <tr>
                  <th>skratka_VUC</th>
                  <th>naz_VUC</th>
                  <th>pocet_obyv</th>
                  <th>rozloha</th>

                </tr>
                <?php

                $query = "SELECT *FROM `kraj`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['skratka_VUC'] ?> </td>
                    <td> <?php echo $row['nazov_VUC'] ?> </td>
                    <td> <?php echo $row['pocet_obyv'] ?> </td>
                    <td> <?php echo $row['rozloha'] ?> </td>


                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'Mesto':
            ?>
              <table>
                <tr>
                  <th>skratka_mesta</th>
                  <th>nazov_mesta</th>
                  <th>okres</th>
                  <th>rozloha_km</th>
                  <th>pocet_obyv</th>

                </tr>
                <?php

                $query = "SELECT *FROM `mesto`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['skratka_mesta'] ?> </td>
                    <td> <?php echo $row['nazov_mesta'] ?> </td>
                    <td> <?php echo $row['okres'] ?> </td>
                    <td> <?php echo $row['rozloha_km'] ?> </td>
                    <td> <?php echo $row['pocet_obyv'] ?> </td>



                  <?php
                } ?>
              </table>
            <?php

                break;

              case 'Objednany_mat':
            ?>
              <table>
                <tr>
                  <th>ID_obj</th>
                  <th>ID_mat</th>
                  <th>mnozstvo</th>


                </tr>
                <?php

                $query = "SELECT *FROM `objednany_mat`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_obj'] ?> </td>
                    <td> <?php echo $row['ID_mat'] ?> </td>
                    <td> <?php echo $row['mnozstvo'] ?> </td>




                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'Objednavka_mat':
            ?>
              <table>
                <tr>
                  <th>ID_obj</th>
                  <th>ID_doda</th>
                  <th>datum_objednania</th>
                  <th>datum_dodania</th>
                  <th>cena</th>
                  <th>miesto_obj</th>



                </tr>
                <?php

                $query = "SELECT *FROM `objednavka_mat`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_obj'] ?> </td>
                    <td> <?php echo $row['ID_doda'] ?> </td>
                    <td> <?php echo $row['dat_objednania'] ?> </td>
                    <td> <?php echo $row['datum_dod'] ?> </td>
                    <td> <?php echo $row['cena'] ?> </td>
                    <td> <?php echo $row['miesto_obj'] ?> </td>




                  <?php
                }
                  ?>
              </table>
            <?php
                break;
              case 'Odberatel':
            ?>
              <table>
                <tr>
                  <th>ID_odberatel</th>
                  <th>nazov</th>
                  <th>ico</th>
                  <th>ulica</th>
                  <th>c_domu</th>
                  <th>mesto</th>
                  <th>psc</th>
                  <th>tel_c</th>
                  <th>email</th>
                  <th>smer</th>


                </tr>
                <?php

                $query = "SELECT *FROM `odberatel`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_odberatel'] ?> </td>
                    <td> <?php echo $row['nazov'] ?> </td>
                    <td> <?php echo $row['ico'] ?> </td>
                    <td> <?php echo $row['ulica'] ?> </td>
                    <td> <?php echo $row['c_domu'] ?> </td>
                    <td> <?php echo $row['mesto'] ?> </td>
                    <td> <?php echo $row['psc'] ?> </td>
                    <td> <?php echo $row['tel_c'] ?> </td>
                    <td> <?php echo $row['email'] ?> </td>
                    <td> <?php echo $row['smer'] ?> </td>




                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'Okres':
            ?>
              <table>
                <tr>
                  <th>skratka_okresu</th>
                  <th>nazov_okresu</th>
                  <th>kraj</th>
                  <th>rozloha_km</th>
                  <th>pocet_obyv</th>

                </tr>
                <?php

                $query = "SELECT *FROM `okres`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['skratka_okresu'] ?> </td>
                    <td> <?php echo $row['nazov_okresu'] ?> </td>
                    <td> <?php echo $row['kraj'] ?> </td>
                    <td> <?php echo $row['rozloha_km'] ?> </td>
                    <td> <?php echo $row['pocet_obyv'] ?> </td>



                  <?php  }
                  ?>
              </table>
            <?php
                break;

              case 'skupina_mat':
            ?>
              <table>
                <tr>
                  <th>id_skup_mat</th>
                  <th>nazov_skup_mat</th>


                </tr>
                <?php

                $query = "SELECT *FROM `skupina_mat`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['id_skup_mat'] ?> </td>
                    <td> <?php echo $row['nazov_skup_mat'] ?> </td>



                  <?php  }
                  ?>
              </table>
            <?php
                break;

              case 'skupina_vyr':
            ?>
              <table>
                <tr>
                  <th>id_skup_vyr</th>
                  <th>nazov_skup_vyr</th>


                </tr>
                <?php

                $query = "SELECT *FROM `skupina_vyr`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['id_skup_vyr'] ?> </td>
                    <td> <?php echo $row['nazov_skup_vyr'] ?> </td>



                  <?php  }
                  ?>
              </table>
            <?php
                break;

              case 'tabulka_faktov1':
            ?>
              <table>
                <tr>
                  <th>ID_vyr</th>
                  <th>ID_mat</th>
                  <th>mnozstvo_materialu</th>
                  <th>jedn_cena</th>
                  <th>typ_mat</th>
                  <th>id_skup_mat</th>
                  <th>typ_vyr</th>
                  <th>id_skup_vyr</th>
                  <th>polozka</th>



                </tr>
                <?php

                $query = "SELECT *FROM `tabulka_faktov1`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_vyr'] ?> </td>
                    <td> <?php echo $row['ID_mat'] ?> </td>
                    <td> <?php echo $row['mnozstvo_materialu'] ?> </td>
                    <td> <?php echo $row['jedn_cena'] ?> </td>
                    <td> <?php echo $row['typ_mat'] ?> </td>
                    <td> <?php echo $row['id_skup_mat'] ?> </td>
                    <td> <?php echo $row['typ_vyr'] ?> </td>
                    <td> <?php echo $row['id_skup_vyr'] ?> </td>
                    <td> <?php echo $row['polozka'] ?> </td>





                  <?php
                }
                  ?>
              </table>
            <?php
                break;
              case 'tabulka_faktov2':
            ?>
              <table>
                <tr>
                  <th>ID_obj</th>
                  <th>ID_vyr</th>
                  <th>mnozstvo</th>
                  <th>vyr_cena</th>
                  <th>pred_cena</th>
                  <th>prijem</th>
                  <th>naklad</th>
                  <th>typ_vyr</th>
                  <th>id_skup_vyr</th>
                  <th>miesto_obj</th>

                  <th>okres</th>
                  <th>kraj</th>
                  <th>rok</th>
                  <th>mesiac</th>
                  <th>den</th>
                  <th>ID_odoberatela</th>
                  <th>smer</th>



                </tr>
                <?php

                $query = "SELECT *FROM `tabulka_faktov2`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_obj'] ?> </td>
                    <td> <?php echo $row['ID_vyr'] ?> </td>
                    <td> <?php echo $row['mnozstvo'] ?> </td>
                    <td> <?php echo $row['vyr_cena'] ?> </td>
                    <td> <?php echo $row['pred_cena'] ?> </td>
                    <td> <?php echo $row['prijem'] ?> </td>
                    <td> <?php echo $row['naklad'] ?> </td>
                    <td> <?php echo $row['typ_vyr'] ?> </td>
                    <td> <?php echo $row['id_skup_vyr'] ?> </td>
                    <td> <?php echo $row['miesto_obj'] ?> </td>
                    <td> <?php echo $row['okres'] ?> </td>
                    <td> <?php echo $row['kraj'] ?> </td>
                    <td> <?php echo $row['rok'] ?> </td>
                    <td> <?php echo $row['mesiac'] ?> </td>
                    <td> <?php echo $row['den'] ?> </td>
                    <td> <?php echo $row['ID_odoberatela'] ?>
                    </td>
                    <td> <?php echo $row['smer'] ?> </td>





                  <?php
                }
                  ?>
              </table>
            <?php
                break;
              case 'tabulka_faktov3':
            ?>
              <table>
                <tr>
                  <th>ID_obj</th>
                  <th>ID_mat</th>
                  < <th>mnozstvo</th>
                    <th>jedn_cena</th>
                    <th>naklad</th>
                    <th>typ_mat</th>
                    <th>id_skup_mat</th>
                    <th>miesto_obj</th>
                    <th>okres</th>
                    <th>kraj</th>
                    <th>rok</th>
                    <th>mesiac</th>
                    <th>den</th>
                    <th>ID_doda</th>
                    <th>smer</th>



                </tr>
                <?php

                $query = "SELECT *FROM `tabulka_faktov3`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['ID_obj'] ?> </td>
                    <td> <?php echo $row['ID_mat'] ?> </td>
                    <td> <?php echo $row['mnozstvo'] ?> </td>
                    <td> <?php echo $row['jedn_cena'] ?> </td>

                    <td> <?php echo $row['naklad'] ?> </td>
                    <td> <?php echo $row['typ_mat'] ?> </td>
                    <td> <?php echo $row['id_skup_mat'] ?> </td>
                    <td> <?php echo $row['miesto_obj'] ?> </td>
                    <td> <?php echo $row['okres'] ?> </td>
                    <td> <?php echo $row['kraj'] ?> </td>
                    <td> <?php echo $row['rok'] ?> </td>
                    <td> <?php echo $row['mesiac'] ?> </td>
                    <td> <?php echo $row['den'] ?> </td>
                    <td> <?php echo $row['ID_doda'] ?> </td>
                    <td> <?php echo $row['smer'] ?> </td>





                  <?php
                }
                  ?>
              </table>
            <?php
                break;

              case 'typ_mat':
            ?>
              <table>
                <tr>
                  <th>id_typ_mat</th>
                  <th>nazov_typ_mat</th>
                  <th>id_skup_mat</th>


                </tr>
                <?php

                $query = "SELECT *FROM `typ_mat`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['id_typ_mat'] ?> </td>
                    <td> <?php echo $row['nazov_typ_mat'] ?> </td>
                    <td> <?php echo $row['id_skup_mat'] ?> </td>



                  <?php  }
                  ?>
              </table>
            <?php
                break;

              case 'Typ_vyrobku':
            ?>
              <table>
                <tr>
                  <th>id_typ_vyr</th>
                  <th>typ_vyro</th>
                  <th>id_skup_vyr</th>


                </tr>
                <?php

                $query = "SELECT *FROM `typ_vyrobku`";
                $query_run = mysqli_query($connection, $query);
                while ($row = mysqli_fetch_array($query_run)) { ?>
                  <tr>

                    <td> <?php echo $row['id_typ_vyr'] ?> </td>
                    <td> <?php echo $row['typ_vyro'] ?> </td>
                    <td> <?php echo $row['id_skup_vyr'] ?> </td>



                  <?php  }
                  ?>
              </table>
          <?php
                break;
            }

          ?>
      </center>
      <br>
      <br>
      <a href="databaza_komplet.php">Spať hore </a>
    </div>
    <footer>
      <p class="footer">Copyright Martin Jankech &COPY 2020;</p>
    </footer>
    <div>
</body>



</body>

</html>