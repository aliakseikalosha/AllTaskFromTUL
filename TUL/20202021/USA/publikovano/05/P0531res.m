%P(X>59.6)=0.2119
%pravdìpodobnost, že je menší než 59.6
Pa=1-0.2119;

%pravdìpodobnost, že je menší než 57,2 je 0.7258
Pb=0.7258;

%vypoètu zskore pro dane pravdìpodobnosti a z nich sestavím rovnice, 
%které potom vypoètu

za=norminv(Pa,0,1);
zb=norminv(Pb,0,1);

%Øeším soustavu rovnic:
%z=(x-my)/sigma pro neznámé mý a sigma. 