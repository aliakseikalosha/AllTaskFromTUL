clc;clear all; close all;
[x,Fs] = audioread('cv04_00.wav');
n = size(x,1);
energie = zeros(ceil(n/100),1);
de = 100;
for i = 1:de:n
    from = i;
    to = min(i+de,n+1)-1;
    index = (i-1)/100+1;
    energie(index) = sum(x(from:to).^2);
    %fprintf("%d\t[%d : %d]\t= %f\n",index,from,min(i+de,n)-1,energie((i-1)/100+1));
end
n = size(energie,1)-1;
diff = zeros(n,1);
for i= 1:n
    diff(i) = energie(i+1) - energie(i);
    fprintf("%d\n",i);
end
energie = energie';
diff = diff';
gcf = figure;
subplot(3,1,1);
plot(x);
ylabel('Signal')
subplot(3,1,2);
plot(energie);
ylabel('energie')
subplot(3,1,3);
plot(diff);
ylabel('energie')
saveas(gcf, 'output.png')