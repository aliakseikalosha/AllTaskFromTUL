%vygeneruji náhodná èísla z jednic z rovnomìrného rozdìlení
A=rand(10000,1);    
subplot(2,2,1)
hist(A)

%vygeneruji náhodná èísla z dvojic z rovnomìrného rozdìlení
B=rand(10000,2);
for i=1:10000
    B_new(i)=(B(i,1)+B(i,2))/2;
end
subplot(2,2,2)
hist(B_new)

%vygeneruji náhodná èísla z dvojic z rovnomìrného rozdìlení
B=rand(10000,2);
for i=1:10000
    B_new(i)=(B(i,1)+B(i,2))/2;
end
subplot(2,2,3)
hist(B_new)

%vygeneruji náhodná èísla z desetic z rovnomìrného rozdìlení
B=rand(10000,10);
for i=1:10000
    B_new(i)=(B(i,1)+B(i,2)+B(i,3)+B(i,4)+B(i,5)+B(i,6)+B(i,7)+B(i,8)+B(i,9)+B(i,10))/10;
end
subplot(2,2,4)
hist(B_new)

