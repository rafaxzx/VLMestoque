INSERT INTO Items (Codeintern, Itemname, Itemspecification, Manufacturerid, UDCnumber, Locationtray, Minstock, Maxstock)
VALUES ("ABCDEFGH", "Item de testes", "Especificação de item de teste", 1, "230", "D", 1, 2);

SELECT * FROM Items INNER JOIN Manufacturers on Manufacturers.Id = Items.Manufacturerid;