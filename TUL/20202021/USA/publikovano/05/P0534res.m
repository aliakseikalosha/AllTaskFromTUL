%pravdìpodobnost, že 1 výrobek je OK
pravd1=1-normcdf(200,180,20);

%dále øešíme pomocí binomického rozdìlení
pravd=binopdf(3,5,pravd1)+binopdf(4,5,pravd1)+binopdf(5,5,pravd1)