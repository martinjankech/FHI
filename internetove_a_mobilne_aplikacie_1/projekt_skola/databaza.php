<!DOCTYPE html>
<html lang="en">

<head>
 <meta charset="UTF-8" />
 <meta name="viewport" content="width=device-width, initial-scale=1.0" />
 <title>HappyBurger</title>
</head>
<link rel="stylesheet" href="style.css" />
<script src="https://kit.fontawesome.com/yourcode.js"></script>

<body>
 <div class="header">
  <h1>Happy Burger</h1>
 </div>

 <nav class="navigacia">
  <a href="index.html">Domov</a>
  <a href="produkty.html">Produkty</a>
  <a href="kontakt.html">Kontakt</a>
  <a href="zadania.html">Zadania</a>
  <a href="databaza.php">Databáza</a>
 </nav>

 <img src="./images/burgre_tacka.jfif" class="tacka_logo" alt="burger tacka logo" />
 <a href="index.html"><img src="./images/happy-burger-logo_zmensene.jpg" class="logo" alt="logo" /></a>

 <br />
 <br />
 <div class="container">
  <form action="" method="POST">
   <table>
    <tr>
     <th>ID_vyr</th>
     <th>nazov_vyrobku</th>
     <th>typ_vyr</th>
     <th>vyr_cena</th>
     <th>pred_cena</th>
    </tr>
   </table>
   <input type="submit" name="search" value="SEARCH DATA">
  </form>
 </div>
</body>
<?php
$servername = "sql2.webzdarma.cz";
$username = "jankechwzcz7860";
$password = "Martin89";
$dbname = "jankechwzcz7860";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
 die("Connection failed: " . mysqli_connect_error());
}
