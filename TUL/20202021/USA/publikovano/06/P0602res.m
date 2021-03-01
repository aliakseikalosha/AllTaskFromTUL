%vygeneruji náhodná èísla z desetic z rovnomìrného rozdìlení
B=rand(10000,10);
for i=1:10000
    B_new(i)=B(i,1)+B(i,2)+B(i,3)+B(i,4)+B(i,5)+B(i,6)+B(i,7)+B(i,8)+B(i,9)+B(i,10);
end
x=0:0.1:10;
fx=10000*exp(-(x-5).^2)/(2*10/12)/sqrt(2*pi*10/12);
hold on
hist(B_new)
plot(x,fx,'r')


hold off