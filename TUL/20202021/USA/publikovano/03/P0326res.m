vstup=importdata('P0326.xlsx')
%v�sledky ve form� struktury, vstupy jsou ulo�eny jako data a d�le v listu1
x=vstup.List1(:,1);

figure;
hist(x)
title('poruchovost za��zen�');
xlabel('��seln� ozna�en� komponenty');
ylabel('po�et poruch');

strhodnota=mean(x)
median_x=median(x)
modus=mode(x)
var(x)

%pro kontrolu
figure;
cdfplot(x)