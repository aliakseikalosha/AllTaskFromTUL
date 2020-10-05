function [X] = VectorRoFi(ro,fi)
X = ChangeSizeVector(RotateVector(OneZeroVector,fi),ro);
end

