%otevøe data uložená v souboru
x=importdata('P0830.mat');

group(1:100)=1;
group(101:200)=2;
group(201:300)=3;
group(301:400)=4;
group(401:500)=5;
group=group';

%Ovìøení Bartlettovým testem
[p,stat]=vartestn(x,group,'on','classical')

%Ovìøení shody støedních hodnot anovou
[h,anovatab,stats]=kruskalwallis(x,group)

%Mnononásobné porovnání
[comparison,means]=multcompare(stats)