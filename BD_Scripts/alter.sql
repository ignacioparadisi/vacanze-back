ALTER TABLE Lugar ADD CONSTRAINT fk_lugar_lugar FOREIGN KEY (fk_lugar) references Lugar(l_id) ON DELETE CASCADE ON UPDATE CASCADE; 

ALTER TABLE Hotel ADD CONSTRAINT fk_hotel_lugar FOREIGN KEY (fk_lugar) references Lugar(l_id) ON DELETE CASCADE ON UPDATE CASCADE;  