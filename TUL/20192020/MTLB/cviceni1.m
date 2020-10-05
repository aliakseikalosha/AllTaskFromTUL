clear;
clc;
%1
disp("Úloha 1")
a = 1:50
%2
disp("Úloha 2")
b = 1:0.2:10
%3
disp("Úloha 3")
A = ones(3)
B = zeros(3)
D = eye(3)
%4
disp("Úloha 4")
M = [A B D]
%5
disp("Úloha 5")
C = [1 -1 1;1 -1 0; -1 0 1]
%6
disp("Úloha 6")
B1 = inv(C)
%7
disp("Úloha 7")
PoPrvcich = C.*B1
Maticove = C*B1
%8
disp("Úloha 8")
VynasobeniMaC = M'*C
%9
disp("Úloha 9")
C(:,2) = rand(size(C(:,2)))
%10
disp("Úloha 10")
C(1,:) = rand(size(C,1),1)
%11
disp("Úloha 11")
C1 = [C(1,end-1) C(1,end)]
%12
disp("Úloha 12")
C(C>=0) = 1
C(C<0) = rand(size(C(C<0)))


