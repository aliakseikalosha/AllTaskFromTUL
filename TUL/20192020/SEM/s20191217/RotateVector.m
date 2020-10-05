function y = RotateVector(x,fi)
s = [cos(fi),-sin(fi);sin(fi), cos(fi)];
y = s*x;
end

