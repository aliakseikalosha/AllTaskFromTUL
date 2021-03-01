%parametry normálního rozdìlení jsou N(10, sqrt(20))

%hodnota distribuèní funkce v bodì 3
pravd=normcdf(3,10,sqrt(20));
pravd=pravd+0.25;

%hledám x
x=norminv(pravd,10,sqrt(20))