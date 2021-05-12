clear all
clc

x=[35,36,36.3,36.8,37.2,37.6,38.3,39.1,39.3,39.6,39.8;37.2,38.1,38.2,37.9,37.6,38.3,39.2,39.4,39.7,39.9,39.9];
data_pred=x(1,:);
data_po=x(2,:);

%test shody rozptylů
[h,p,ci,stat]=vartest2(data_pred,data_po)
%rozptyly jsou shodné 

%H0: střední hodnoty jsou shodné    H1:střední hodnoty nejsou shodné
[h,p,ci,stat]=ttest2(data_pred,data_po)