%M=200
%N=10
%po�et losovanych: K=30
%uh�dnul jsem: x=5

%nevrac�me zp�t do bal��ku - hypergeometrick� rozd�len�
vysledek=hygepdf(5,200,30,10)

%vrac�me do bal��ku - binomick� rozd�len�
%uh�dneme 5 ��sel, 10 ��sel tipujeme, 30/200 �ance na uh�dnut�
vysledek=binopdf(5,10,30/200)

%odhad st�edn� hodnoty je 1.5
vysledek_1=hygepdf(1,200,30,10);
vysledek_2=hygepdf(2,200,30,10);
if vysledek_1>vysledek_2
    vysledek=1
else
    vysledek=2
end
