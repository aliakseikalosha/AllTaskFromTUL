clc;clear all;close all;
B = [1,3;2,0;1,2]
A = [1,0,3;2,1,1]
C = zeros(size(B, 2), size(A, 1));
for i=1:size(C,1)
    for j =1:size(C,2)
        C(i,j) = sum(A(i,:).*B(:,j)'); 
    end
end
disp("A*B= ")
disp(C)

C = zeros(size(B, 1), size(A, 2));
for i=1:size(C,1)
    for j =1:size(C,2)
        C(i,j) = sum(B(i,:).*A(:,j)'); 
    end
end

disp("B*A= ")
disp(C)