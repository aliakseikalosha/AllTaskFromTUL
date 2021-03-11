vstup=importdata('P0323.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1
x=vstup.data.List1(:,1);

hist(x)
title('poruchovost zaøízení');
xlabel('èíselné oznaèení komponenty');
ylabel('poèet poruch');
