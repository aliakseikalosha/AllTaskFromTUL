%pravdìpodobnost jednoho prvku urèím z pravdìpodobnostní funkce
vysledek_a=binopdf(2,5,0.5)
%je zde více prvkù, proto z distribuèní funkce. Poèítám doplnìk k
%distribuèní funkci (stavy 0,1,2,3), protože je to snažší. Lze i pøes
%binopdf (4,5,0.5)+binopdf(5,5,0.5)
vysledek_b=1-binocdf(3,5,0.5)