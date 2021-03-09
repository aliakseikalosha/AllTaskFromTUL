clear all
clc

%poèet kombinací èervených, kombinace bez opakování
cervene=nchoosek(8,5);
%poèet kombinací žaludù, kombinace bez opakování
zaludy=nchoosek(8,4);
%výsledek je souèin kombinací èervených a žaludù, protože se nijak
%neovlivòují (pokusy jsou nezávislé)

vysledek=cervene*zaludy

