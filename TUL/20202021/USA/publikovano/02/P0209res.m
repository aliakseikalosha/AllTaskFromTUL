%chceme pravd�podobnost, �e budou dv� vstupenky budou po 100 K� a posledn�
%v jin� cen�
%obdobn� dv� vstupenky budou po 150 K�, dv� vstupenky po 200 K�

vstupenek=10;
losovano=3;
vstupenek100=5;

P100=(nchoosek(vstupenek100,2)*nchoosek(vstupenek-vstupenek100,1))/nchoosek(vstupenek,losovano)

vstupenek150=3;
vstupenek200=2;
P150=(nchoosek(vstupenek150,2)*nchoosek(vstupenek-vstupenek150,1))/nchoosek(vstupenek,losovano)
P200=(nchoosek(vstupenek200,2)*nchoosek(vstupenek-vstupenek200,1))/nchoosek(vstupenek,losovano)

P=P100+P150+P200
