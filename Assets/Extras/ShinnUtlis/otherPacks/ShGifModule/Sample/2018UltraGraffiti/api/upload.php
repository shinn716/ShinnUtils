<?php

$path_parts = pathinfo($path); 
   if(isset($_FILES['theFile']))
   {
      print("Success! ");
      print("tmpName: " . $_FILES['theFile']['tmp_name'] . " ");
      print("size: " . $_FILES['theFile']['size'] . " ");
      print("mime: " . $_FILES['theFile']['type'] . " ");
      print("name: " . $_FILES['theFile']['name'] . " ");


   move_uploaded_file($_FILES['theFile']['tmp_name'], "upload/" . $_FILES['theFile']['name']);

   } else
   {
      print("Failed!");
   }
?>