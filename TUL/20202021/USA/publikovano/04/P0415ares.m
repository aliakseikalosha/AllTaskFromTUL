clear all
clc
%a až c pokus bude ukončen právě při splnění 3 dárce
%d proběhne 10 pokusů a chceme zjistit pravd, že mezi nimi budou 3 A+

%a) desátý dárce bude právě 3. úspěšný
vysl_a=nbinpdf(7,3,0.35)

%b)bude potřeba do 9 dárců včetně
vysl_b=nbincdf(6.5,3,0.35);
vysl_b=1-vysl_b

%c) 
vysl_c=nbincdf(7.5,3,0.35)-nbincdf(2.5,3,0.35)

%d)
vysl_d=binopdf(3,10,0.35)