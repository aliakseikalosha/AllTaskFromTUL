data0(1:3)=0;
data1(1:10)=1;
data2(1:15)=2;
data3(1:12)=3;
data4(1:17)=4;
data5(1:10)=5;
data6(1:10)=6;
data7(1:9)=7;
data8(1:5)=8;
data9(1:5)=9;
data10(1:4)=10;
data11(1:5)=12; %pøedpokládáme, e støední hodnota pro vìtší ne 10 bude asi 12. 
%Vloení èísla 11 není také na závadu

x=[data0,data1,data2,data3,data4,data5,data6,data7,data8,data9,data10,data11];
str_hod=mean(x);

[h,p,stats]=chi2gof(x,'cdf',{@poisscdf,mean(x)})
