vstup=importdata('P0313.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1
x=vstup.data.List1(1,:);
y=vstup.data.List1(2,:);

%vypoète støední hodnotu
strhodnota=0;
for i=1:length(x)
    strhodnota=strhodnota+x(i)*y(i);
end
strhodnota

%vypoète rozptyl
rozptyl=0;
for i=1:length(x)
   rozptyl=rozptyl+(x(i)-strhodnota)^2*y(i);
end
rozptyl

%smìrodatná odchylka je odmocninou z rozptylu
smerodch=sqrt(rozptyl)

%vypoète šikmost a špièatost z tøetího a ètvrtého momentu
tretimoment=0;
ctvrtymoment=0;
for i=1:length(x)
   tretimoment=tretimoment+(x(i)-strhodnota)^3*y(i);
   ctvrtymoment=ctvrtymoment+(x(i)-strhodnota)^4*y(i);
end
sikmost=tretimoment/smerodch^3
spicatost=ctvrtymoment/smerodch^4


