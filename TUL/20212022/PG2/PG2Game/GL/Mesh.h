#pragma once

#include <vector>
#include <string>
#include <glm/vec3.hpp>
#include <glm/vec2.hpp>
#include "shape.h"
#include "Vertex.h"

#define array_cnt(a) ((unsigned int)(sizeof(a)/sizeof(a[0])))
#define LINE_BUF_SIZE 255

//
// Created by Aliaksei Kalosha on 11.04.2022.
//

class Mesh {
private:

    errno_t fopen_s(FILE **f, const char *name, const char *mode) {
        errno_t ret = 0;
        assert(f);
        *f = fopen(name, mode);
        /* Can't be sure about 1-to-1 mapping of errno and MS' errno_t */
        if (!*f)
            ret = errno;
        return ret;
    }

public:
    std::vector<Vertex> vertex;
    std::vector<unsigned int> index;

    void loadFloor(glm::vec2 bottomLeft, float textureWidth, float size) {
        quadH(vertex, index, size, bottomLeft, textureWidth);
    }


    void loadOBJ(std::string model_path, glm::vec2 bottomLeft, float textureWidth, float size) {

        std::vector<unsigned int> vertexIndices, uvIndices, normalIndices;
        std::vector<glm::vec3> temp_vertices;
        std::vector<glm::vec2> temp_uvs;
        std::vector<glm::vec3> temp_normals;

        FILE *file;
        fopen_s(&file, model_path.c_str(), "r");
        if (file == NULL) {
            printf("Impossible to open the file !\n");
            exit(EXIT_FAILURE);
        }

        while (true) {

            char lineHeader[LINE_BUF_SIZE];
            int res = fscanf(file, "%s", lineHeader, array_cnt(lineHeader));
            if (res == EOF) {
                break;
            }

            if (strncmp(lineHeader, "v", LINE_BUF_SIZE) == 0) {
                glm::vec3 vertex;
                fscanf(file, "%f %f %f\n", &vertex.x, &vertex.y, &vertex.z);
                temp_vertices.push_back(vertex);
            } else if (strncmp(lineHeader, "vt", LINE_BUF_SIZE) == 0) {
                glm::vec2 uv;
                fscanf(file, "%f %f\n", &uv.y, &uv.x);
                temp_uvs.push_back(uv);
            } else if (strncmp(lineHeader, "vn", LINE_BUF_SIZE) == 0) {
                glm::vec3 normal;
                fscanf(file, "%f %f %f\n", &normal.x, &normal.y, &normal.z);
                temp_normals.push_back(normal);
            } else if (strncmp(lineHeader, "f", LINE_BUF_SIZE) == 0) {
                std::string vertex1, vertex2, vertex3;
                unsigned int vertexIndex[3], uvIndex[3], normalIndex[3];
                int matches = fscanf(file, "%d/%d/%d %d/%d/%d %d/%d/%d\n", &vertexIndex[0], &uvIndex[0], &normalIndex[0],
                                     &vertexIndex[1], &uvIndex[1], &normalIndex[1], &vertexIndex[2], &uvIndex[2],
                                     &normalIndex[2]);
                if (matches != 9) {
                    printf("File can't be read by simple parser :( Try exporting with other options\n");
                    exit(-1);
                }
                vertexIndices.push_back(vertexIndex[0]);
                vertexIndices.push_back(vertexIndex[1]);
                vertexIndices.push_back(vertexIndex[2]);
                uvIndices.push_back(uvIndex[0]);
                uvIndices.push_back(uvIndex[1]);
                uvIndices.push_back(uvIndex[2]);
                normalIndices.push_back(normalIndex[0]);
                normalIndices.push_back(normalIndex[1]);
                normalIndices.push_back(normalIndex[2]);
            }
        }

        fclose(file);

        // unroll from indirect to direct vertex specification
        std::vector<glm::vec3> out_vertices;
        std::vector<glm::vec2> out_uvs;
        std::vector<glm::vec3> out_normals;

        for (unsigned int u = 0; u < vertexIndices.size(); u++) {
            unsigned int vertexIndex = vertexIndices[u];
            glm::vec3 vertex = temp_vertices[vertexIndex - 1];
            out_vertices.push_back(vertex);
        }
        for (unsigned int u = 0; u < uvIndices.size(); u++) {
            unsigned int uvIndex = uvIndices[u];
            glm::vec2 uv = temp_uvs[uvIndex - 1];
            out_uvs.push_back(uv);
        }
        for (unsigned int u = 0; u < normalIndices.size(); u++) {
            unsigned int normalIndex = normalIndices[u];
            glm::vec3 normal = temp_normals[normalIndex - 1];
            out_normals.push_back(normal);
        }


        if ((out_vertices.size() != out_uvs.size()) || (out_uvs.size() != out_normals.size())) {
            std::cerr << "Size of arrays doesn't match in '" << model_path << "' Load failed.\n";
            std::cerr << "  Vertices: " << out_vertices.size() << ", UVs: " << out_uvs.size() << ", normals: "
                      << out_normals.size() << '\n';
            exit(-1);
        }

        for (int i = 0; i < out_vertices.size(); i += 3) {
            Vertex v1 = {out_vertices[i] * size, out_uvs[i] * textureWidth + bottomLeft, out_normals[i]};
            Vertex v2 = {out_vertices[i + 1] * size, out_uvs[i + 1] * textureWidth + bottomLeft, out_normals[i + 1]};
            Vertex v3 = {out_vertices[i + 2] * size, out_uvs[i + 2] * textureWidth + bottomLeft, out_normals[i + 2]};

            vertex.push_back(v1);
            vertex.push_back(v2);
            vertex.push_back(v3);

            index.push_back(i);
            index.push_back(i + 1);
            index.push_back(i + 2);
        }
    }
};
