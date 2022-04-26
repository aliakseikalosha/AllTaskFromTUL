#include <string>
#include <GL/glew.h>
#include <glm/glm.hpp>
#include <iostream>

#include "OBJloader.h"
#include "mesh.h"
#include "texture.h"

#define array_cnt(a) ((unsigned int)(sizeof(a)/sizeof(a[0])))
#define LINE_BUF_SIZE 255

errno_t fopen_s(FILE **f, const char *name, const char *mode) {
    errno_t ret = 0;
    assert(f);
    *f = fopen(name, mode);
    /* Can't be sure about 1-to-1 mapping of errno and MS' errno_t */
    if (!*f)
        ret = errno;
    return ret;
}

mesh loadOBJ(std::string model_path) {
    mesh tmpmesh;

    std::vector<vertex> vertices;
    std::vector<GLuint> indices;

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

    while (1) {

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

    for (int i = 0; i < out_vertices.size(); i = i + 3) {
        vertex v1 = {out_vertices[i], out_uvs[i], out_normals[i]};
        vertex v2 = {out_vertices[i + 1], out_uvs[i + 1], out_normals[i + 1]};
        vertex v3 = {out_vertices[i + 2], out_uvs[i + 2], out_normals[i + 2]};

        vertices.push_back(v1);
        vertices.push_back(v2);
        vertices.push_back(v3);

        indices.push_back(i);
        indices.push_back(i + 1);
        indices.push_back(i + 2);
    }

    tmpmesh.init(GL_TRIANGLES, vertices, indices);

    return tmpmesh;
}

mesh loadOBJtex(std::string obj_path, std::string texture_path) {
    mesh out_mesh = loadOBJ(obj_path);

    if (!loadTexture(out_mesh, texture_path)) {
        std::cerr << "Failed loading texture '" << texture_path << "'.\n";
        exit(-1);
    }

    return out_mesh;
}

bool loadTexture(mesh &out_mesh, std::string texture_path) {
    if (texture_path.empty()) {
        return false;
    }

    out_mesh.tex_ID = textureInit(texture_path.c_str(), false);

    return true;
}
