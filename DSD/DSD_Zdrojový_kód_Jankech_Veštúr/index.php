<?php
include 'partials/header.php';
require_once '_inc/config.php'
?>


<?php
        /* Include and initialize DB class */
        require_once 'classes/DB.class.php';
        require_once './_inc/config.php';
        $config=new config;
        $config->connect();
        session_start(); // session_start() -> vytvorí reláciu alebo obnoví aktuálnu na základe identifikátora relácie odovzdaného prostredníctvom požiadavky GET alebo POST alebo odovzdaného prostredníctvom súboru cookie.
        // zabranenie vstupu neopravnen7m uzivatelom
        if(!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true){
            header("location: login.php");
            exit;
        }
         $db = new DB; 
        

        if( !empty( $config->getNotaviableconnection() )){

            foreach( $config->getNotaviableconnection() as $value ){

                echo '<div class="alert alert-danger" role="alert">
                    uzol '.$value.' je odpojený !
                    </div>';

            }


        }
        $updateip = $config->synchronize();

        if( !empty( $updateip )){ 
            foreach( $updateip as $value ){
                echo '<div class="alert alert-success" role="alert">
                uzol '.$value.' bol synchronizovany s ostatnymi uzlami ! </div>';
            }
        }

        /* Ziskanie udajov */
        $movies = $db->getRows('movies');
    ?>

<div class="container">
    <div class="row">
        <div class="col-md-12 head">
            <h5 class="text-center p-3 mb-2 bg-secondary text-white"> <?php echo $_SERVER['SERVER_NAME'] ?></h5>
            <h1 class="text-center mt-3 mb-5 text-primary">Ahoj
                <b><?php echo htmlspecialchars( $_SESSION[ "username" ]); ?></b>. Vitaj
                v
                našej databáze na filmy.
            </h1>
            <div class="col-md-3 mt-4 mb-2" >
                        <div class=" "  >
                            <div class="input-group">
                            <span class="input-group-append">
                                    <div class="input-group-text bg-transparent"><i class="fa fa-search"></i></div>
                                </span>
                                <input class="form-control " type="search" value="search" id="my-input" onkeyup="search()">
                                
                            </div>
                        </div>
                    </div>
            <!-- Add link -->
            <div class="float-right">
                <a href="javascript:void(0);" class="btn btn-success mb-2" data-type="add" data-toggle="modal"
                    data-target="#modalUserAddEdit"><i class="plus"></i> Pridat Film</a>
                <a href="logout.php" class="btn  btn-outline-info mb-2"
                    onclick="return confirm('Naozaj sa chcete odhlásiť ?')">Odhlásiť sa z učtu</a>
             
            </div>
        </div>
        <div class="statusMsg"></div>
        <!-- List the users -->
        <table id="my-table" class="table table-striped table-dark">
            <thead class="thead-light">
                <tr>
                    <th>Id</th>
                    <th>Názov</th>
                    <th>Meno Režiséra</th>
                    <th>Hlavný Herec</th>
                    <th>Hodnotenie Imdb</th>
                    <th>Pridal Uživatel</th>
                    <th>Uzol</th>
                    <th>Možnosti</th>
                </tr>
            </thead>
            <tbody id="movieData">
                <?php if( !empty($movies) ) { foreach( $movies as $row ){ ?>
                <tr>
                    <td><?php echo '#'. $row['Id']; ?></td>
                    <td><?php echo $row['Name_Movie']; ?></td>
                    <td><?php echo $row['Name_Director']; ?></td>
                    <td><?php echo $row['Main_Actor']; ?></td>
                    <td><?php echo $row['Rating_Imdb']; ?></td>
                    <td><?php echo $row['Added_By_User']; ?></td>
                    <td><?php echo $row['Node_Created']; ?></td>
                    <td>
                    <?php
                     if (!empty ($config->getNotaviableconnection()) && in_array($row['Node_Created'], $config->getNotaviableconnection())){
                        
                         ?>
                            <a href="javascript:void(0);" class="btn btn-primary disabled"  rowID="<?php echo $row['Id']; ?>" data-type="edit" data-toggle="modal" data-target="#modalUserAddEdit" >Upraviť</a>
                            <a href="javascript:void(0);" class="btn btn-outline-danger disabled" onclick="return confirm('Naozaj chcete odstrániť data ?')?movieAction('delete', '<?php echo $row['Id']; ?>'):false;">Odstraniť</a>
                        </td>
                        <?php
                         }
                         else{?>
                            <a href="javascript:void(0);" class="btn btn-primary "  rowID="<?php echo $row['Id']; ?>" data-type="edit" data-toggle="modal" data-target="#modalUserAddEdit" >Upraviť</a>
                            <a href="javascript:void(0);" class="btn btn-outline-danger" onclick="return confirm('Naozaj chcete odstrániť data ?')?movieAction('delete', '<?php echo $row['Id']; ?>'):false;">Odstraniť</a>
                            </td>
                            <?php

                         }
                         
                         
                        ?>
                </tr>
                <?php } }else{ ?>
                <tr><td colspan="5">Žiadny záznam</td></tr>
                <?php } ?>
            </tbody>
        </table>
        </div>
    </div>
    
</div>

<!-- Modal Add and Edit Form -->
<div class="modal fade" id="modalUserAddEdit" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <!-- Modal Header -->
            <div class="modal-header">
                <h4 class="modal-title">Pridať Film do DB</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>

            <!-- Modal Body -->
            <div class="modal-body">
                <div class="statusMsg"></div>
                <form role="form">
                    <div class="form-group">
                        <label for="Name_Movie">Názov</label>
                        <input type="text" class="form-control" name="Name_Movie" id="Name_Movie"
                            placeholder="Vložte meno filmu.">
                    </div>
                    <div class="form-group">
                        <label for="Name_Director">Meno Režiséra</label>
                        <input type="text" class="form-control" name="Name_Director" id="Name_Director"
                            placeholder="Vložte meno režiséra.">
                    </div>
                    <div class="form-group">
                        <label for="Main_Actor">Hlavný Herec</label>
                        <input type="text" class="form-control" name="Main_Actor" id="Main_Actor"
                            placeholder="Vložte meno hlavného herca.">
                    </div>
                    <div class="form-group">
                        <label for="Rating_Imdb">Hodnotenie Imdb</label>
                        <input type="number" class="form-control" name="Rating_Imdb" id="Rating_Imdb"
                            placeholder="Vložte rating filmu podľa imdb.">
                    </div>
                    

                    <input type="hidden" class="form-control" name="Id" id="Id" />
                </form>
            </div>

            <!-- Modal Footer -->
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Zavrieť</button>
                <button type="button" class="btn btn-success" id="movieSubmit">Potvrdiť</button>
            </div>
        </div>
    </div>
</div>
<script>function search() {
  // Declare variables
  var input, filter, table, tr, td, i, txtValue;
  input = document.getElementById("my-input");
  filter = input.value.toUpperCase();
  table = document.getElementById("my-table");
  tr = table.getElementsByTagName("tr");

  // Loop through all table rows, and hide those who don't match the search query
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[1];
    if (td) {
      txtValue = td.textContent || td.innerText;
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        tr[i].style.display = "";
      } else {
        tr[i].style.display = "none";
      }
    }
  }
}</script>

<script src="assets/js/script.js"></script>

</body>

</html>