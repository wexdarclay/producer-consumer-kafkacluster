# Instructions for Running this project
## Parts
    1. docker-compose: this compose file contains everthing needed to run a basic kafka cluster.
        - It contains Zookeeper, which is used to run the broker and schema registry.
        - It contains a broker, used to transfer the messages from producer to consumer. The broker includes scripts to build two topics.
        - It contains the schema registry.
    
    2. schema.avsc file:
        - This file is a stub for the avro-producer service inside the docker file.  This is currently not working and is commented out.    
    
## Instructions for creating a schema and adding it to the schema registry
    1. Ensure that your docker compose file is up and running.  Zookeeper, Broker, and Schema-Registry should be running in a container.
    
    2. In a powershell window run: 
            docker exec -it schema-registry bash
        -this runs a bash command instance within the schema-registry container.
    
    3. Producing a Schema and running a cli producer
        -now within that bash command instance run the following command:
            kafka-avro-console-producer --broker-list broker:29092 --topic test-topic1 --property value.schema='{"type":"record","name":"person","namespace": "com.example","fields":[{"name":"id","type":"int"},{"name":"fname","type":"string"},{"name":"lname","type":"string"},{"name":"email","type":"string"}]}'
        -this will connect to the broker at port 29092 and assing the schema to "test-topic1".

    4. Once the "Schema and running a CLI Producer" command is complete, you will be given a messaging instance to send messages.
        -Note: When sending a message, the message syntax is like so:
            {"id": 1, "fname": "John", "lname": "Doe", "email": "johndoe@example.com"}
     
    5. Consuming a Schema
        -within the bash command instance (see #2 above), you can create a consumer using the following command
            kafka-avro-console-consumer --bootstrap-server broker:29092 --topic test-topic1 --from-beginning