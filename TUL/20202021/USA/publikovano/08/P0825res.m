x1=[18,19,19,19,20,21,21,22,22,23,23,24,24,24,25,25,25,26,26,26,27,28];
x2=[17,18,18,19,19,20,21,21,22,22,22,23,23,23,23,24,24,25,25,26,26,27,28,29] ;
x3=[16,17,18,18,18,19,20,20,20,20,21,21,21,22,23,23,23,24,25,26,27,27,28,28,29,31];
x4=[14,15,16,16,17,18,19,20,22,22,22,23,24,25,25,27,27,27,28,28,28,31,31,33,34];

x=[x1,x2,x3,x4]';

%vytvoøení vektoru skupin
m1=length(x1);
m2=length(x2);
m3=length(x3);
m4=length(x4);
group1(1:m1)=1;
group2(1:m2)=2;
group3(1:m3)=3;
group4(1:m4)=4;

group=[group1,group2,group3,group4]';

%Bartlettuv test
[p,stats]=vartestn(x,group,'on','classical')
%Leveneuv test
[p,stats]=vartestn(x,group,'on','robust')