vstup=importdata('P0907.xlsx')
%výsledky ve formì struktury, vstupy jsou uloženy jako data a dále v listu1
x=vstup.List1(:,1);

wblplot(x)

%ad a)
edges=[0,100,200,300,400,500,700,1000,1500,2000,3000];
[h,p,stats]=chi2gof(x,'cdf',{@expcdf,mean(x)},'edges',edges)

edges=[0,300,500,1000,1500,2000,3000];
[h,p,stats]=chi2gof(x,'cdf',{@expcdf,mean(x)},'edges',edges)

%ad c)
edges=[0,100,300,500,1000,1500,2000,3000];
[h,p,stats]=chi2gof(x,'cdf',{@expcdf,1/0.009},'edges',edges)

%ad d)
[h,p,stats]=chi2gof(x,'cdf',{@normcdf,mean(x),std(x)})

%ad e]
normplot(x)