clear all
clc

vstup=importdata('P0326.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1
x=vstup.List1(:,1);

strhodnota=mean(x)
median_x=median(x)
modus=mode(x)
rozptyl=var(x)
smodch=std(x)

%pro kontrolu
hold on
subplot(2,1,1)
hist(x)
subplot(2,1,2)
cdfplot(x)
hold off