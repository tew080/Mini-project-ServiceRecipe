# Mini-project-ServiceRecipe
 
List service name

- GetAllFood
- SearchFoodName
- AddDataFood
- UpdateDataFood
- DeleteDataFood

# Is in Diretory

/ServiceRecipe/IService1.cs <br>
/ServiceRecipe/Service1.svc.cs <br>
/ServiceRecipe/Web.config

# CREATE Database

CREATE DATABASE wikifood CHARACTER SET utf8 COLLATE utf8_thai_520_w2;

# CREATE TABLE

CREATE TABLE wikifoods (
  food_id INT AUTO_INCREMENT PRIMARY KEY,
  food_name varchar(50) NOT NULL,
  raw_material varchar(2055) NOT NULL,
  recipe varchar(2055) NOT NULL)COLLATE=utf8_thai_520_w2;