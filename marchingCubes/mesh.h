#ifndef MESH_H
#define MESH_H

#include <glad/glad.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <string>
#include <vector>

#include "shader.h"

using namespace std;

struct Vertex {
    glm::vec3 Position;
    glm::vec3 Color;
    glm::vec3 Normal;
};

class Mesh {
    public:
        // mesh data
        vector<Vertex>       vertices;
        vector<unsigned int> indices;

        Mesh(vector<Vertex> vertices, vector<unsigned int> indices) {
            this->vertices = vertices;
            this->indices = indices;

            setupMesh();
        }
        void Draw() {
            glBindVertexArray(VAO);
            glDrawElements(GL_TRIANGLES, indices.size(), GL_UNSIGNED_INT, 0);
            glBindVertexArray(0);
        }
    private:
        //  render data
        unsigned int VAO, VBO, EBO;

        void setupMesh() {
            glGenVertexArrays(1, &VAO);
            glGenBuffers(1, &VBO);
            glGenBuffers(1, &EBO);

            glBindVertexArray(VAO);
            glBindBuffer(GL_ARRAY_BUFFER, VBO);

            glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(Vertex), &vertices[0], GL_STATIC_DRAW);

            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned int),&indices[0], GL_STATIC_DRAW);

            // vertex positions
            glEnableVertexAttribArray(0);
            glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)0);
            // vertex colors
            glEnableVertexAttribArray(1);
            glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, Color));
            // vertex normals
            glEnableVertexAttribArray(2);
            glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, Normal));
            
            glBindVertexArray(0);
        }
};

#endif