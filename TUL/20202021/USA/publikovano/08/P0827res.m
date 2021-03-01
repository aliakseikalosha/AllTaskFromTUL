x1=[18,19,19,19,20,21,21,22,22,23,23,24,24,24,25,25,25,26,26,26,27,28];
x2=[17,18,18,19,19,20,21,21,22,22,22,23,23,23,23,24,24,25,25,26,26,27,28,29] ;
x3=[16,17,18,18,18,19,20,20,20,20,21,21,21,22,23,23,23,24,25,26,27,27,28,28,29,31];
x4=[14,15,16,16,17,18,19,20,22,22,22,23,24,25,25,27,27,27,28,28,28,31,31,33,34];

x=[x1,x2,x3,x4]';

%vytvoøení vektoru skupin
l1=length(x1);
l2=length(x2);
l3=length(x3);
l4=length(x4);
group1(1:l1)=1;
group2(1:l2)=2;
group3(1:l3)=3;
group4(1:l4)=4;

group=[group1,group2,group3,group4]';

%Kruskall Wallisùv test
[p,anovatab,stats]=kruskalwallis(x,group)
%pro porovnání výsledkù i ANOVA
[p,anovatab,stats]=anova1(x,group)