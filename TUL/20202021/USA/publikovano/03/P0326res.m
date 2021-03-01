vstup=importdata('P0326.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1
x=vstup.List1(:,1);

figure;
hist(x)
title('poruchovost zaøízení');
xlabel('èíselné oznaèení komponenty');
ylabel('poèet poruch');

strhodnota=mean(x)
median_x=median(x)
modus=mode(x)
var(x)

%pro kontrolu
figure;
cdfplot(x)