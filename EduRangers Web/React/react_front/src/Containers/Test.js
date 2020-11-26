import React, {useState, useEffect}  from 'react';
    import axios from 'axios';
    import './Test.css';
    import Form from "react-bootstrap/Form";
    import Button from "react-bootstrap/Button";
    import history from "../GlobalHistory/GlobalHistory"

export default function Test(props){
    const [test, setTest] = useState();  
        const [name, setName] = useState(); 
        const [desc, setDesc] = useState(); 
        const [isLoading, setIsLoading] = useState(true);
        const [error, setError] = useState();

        function GetTest() {
            axios({method: 'get',
            url: `https://localhost:44327/api/Test/?id=${props.match.params.id}`,
            headers: {'Content-Type': 'application/json'}})
              .then(
                (result) => {
                  console.log("RESULT: ", result.data)
                  setTest(result.data)
                  setIsLoading(false);
                }
              ).catch(error => {setError(error);
                                setIsLoading(false);}) 
          }

          function handleSubmit(event) {
            event.preventDefault();
            const testModel = {
                    TestDescription: name,
                    TestName: desc
                };
            JSON.stringify(testModel);
            axios({
              method: 'put',
              url: `https://localhost:44327/api/Test/?id=${props.match.params.id}`,
              data: JSON.stringify(testModel),
              maxContentLength: Infinity,
              maxBodyLength: Infinity,
              headers: {'Content-Type': 'application/json'}
            })
              .then(res => {
                console.log("RESPONSE ", res);
                console.log(res.data);
                if(res.data.Succedeed){
                  alert("The test was changed succesfully");
                }  
                else alert("The test wasn`t changed");
              }) 
          }


        useEffect(() => {
             GetTest();
        }, []);

        if(isLoading){
            return(
            <p>Loading...</p>
            )
        }
        else if(error){
            return(<p>Error</p>)
        }
        
        

        const Questions = test.Questions.map((item => <ul key = {item.Id}><li>{item.QuestionText}<Button onClick={() => history.push(`/question/${item.Id}`)}>Edit</Button><Button onClick={() => {axios.delete(`https://localhost:44327/api/Question/?id=${item.Id}`)
        .then(res => {
          console.log(res);
          console.log(res.data);
        })}}>Delete</Button></li><li>{item.Answers.AnswersString}</li></ul>));
        console.log({Questions});

        return(
        <div className="Test">
          <Button onClick={() => history.push(`/addquestion/${test.Id}`)}>Add question</Button>
          <Form onSubmit={handleSubmit}>
            <Form.Group size="lg" controlId="name">
        <Form.Label><h3>Test</h3></Form.Label>
        <Form.Control
            type="text"
            defaultValue={test.TestName}
        onChange={(e) => setName(e.target.value)}
        />
        </Form.Group>
        <Form.Group size="lg" controlId="desc">
        <Form.Label><h6>Description</h6></Form.Label>
        <Form.Control
            type="text"
            defaultValue={test.TestDescription}
        onChange={(e) => setDesc(e.target.value)}
        />
        </Form.Group>
        <Button block size="lg" type="submit">
        Save
        </Button>
    </Form>
        <br />
        <br />
        <h6>Questions:</h6>
        {Questions}
        </div>
        )
    
}