vstup=importdata('P0313.xlsx')
%v�sledky ve form� struktury, vstupy jsou ulo�eny jako data a d�le v listu1
x=vstup.data.List1(1,:);
y=vstup.data.List1(2,:);

%vypo�te st�edn� hodnotu
strhodnota=0;
for i=1:length(x)
    strhodnota=strhodnota+x(i)*y(i);
end
strhodnota

%vypo�te rozptyl
rozptyl=0;
for i=1:length(x)
   rozptyl=rozptyl+(x(i)-strhodnota)^2*y(i);
end
rozptyl

%sm�rodatn� odchylka je odmocninou z rozptylu
smerodch=sqrt(rozptyl)

%vypo�te �ikmost a �pi�atost z t�et�ho a �tvrt�ho momentu
tretimoment=0;
ctvrtymoment=0;
for i=1:length(x)
   tretimoment=tretimoment+(x(i)-strhodnota)^3*y(i);
   ctvrtymoment=ctvrtymoment+(x(i)-strhodnota)^4*y(i);
end
sikmost=tretimoment/smerodch^3
spicatost=ctvrtymoment/smerodch^4


