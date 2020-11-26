import React, {useState, useEffect}  from 'react';
    import axios from 'axios';
    import './Question.css';
    import Form from "react-bootstrap/Form";
    import Button from "react-bootstrap/Button";
    import { Formik, Field, Form as Form1} from "formik";
    import history from "../GlobalHistory/GlobalHistory"

    export default function Question(props){
        const [question, setQuestion] = useState();  
        const [answer, setAnswer] = useState(); 
        const [name, setName] = useState(); 
        const [isLoading, setIsLoading] = useState(true);
        const [error, setError] = useState();
        //let Answers

        function GetQuestion() {
            axios({method: 'get',
            url: `https://localhost:44327/api/Question/?id=${props.match.params.id}`,
            headers: {'Content-Type': 'application/json'}})
                .then(
                (result) => {
                    console.log("RESULT: ", result.data)
                    setQuestion(result.data)
                    setIsLoading(false);
                }
                ).catch(error => {setError(error);
                                setIsLoading(false);}) 
            }

            function handleSubmit(event) {
            event.preventDefault();
            const questionModel = {
                QuestionText: name
            };
            JSON.stringify(questionModel);
            axios({
                method: 'put',
                url: `https://localhost:44327/api/Question/?id=${props.match.params.id}`,
                data: JSON.stringify(questionModel),
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

            function handleSubmit2(event) {
                event.preventDefault();
                console.log(event.target);
                console.log(event.target.value);
                const target = event.target;
               /* const value = target.type === 'checkbox' ? target.checked : target.value;
                const name = target.name;
            

                const answerModel = {
                    QuestionText: 
                };
                JSON.stringify(answerModel);
                axios({
                    method: 'put',
                    url: `https://localhost:44327/api/Answer/?id=${event.Id}`,
                    data: JSON.stringify(answerModel),
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
                    }) */
                }


        useEffect(() => {
            GetQuestion();
        }, []);

        if(isLoading){
            return(
            <p>Loading...</p>
            )
        }
        else if(error){
            return(<p>Error</p>)
        }
        
        

       const Answers = question.Answers.map((item =>   <Formik key = {item.Id} 
        initialValues={{ AnswerText: `${item.AnswerText}`, IsCorrect: `${item.IsCorrect}`, Id: `${item.Id}` }}
        onSubmit={async values => {
            const AnSwer = {
                AnswerText: values.AnswerText,
                IsCorrect: values.IsCorrect
            }
          await new Promise(resolve => setTimeout(resolve, 500));
          axios({
            method: 'put',
            url: `https://localhost:44327/api/Answer/?id=${values.Id}`,
            data: JSON.stringify(AnSwer),
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
          alert(JSON.stringify(AnSwer));

        }}> 
        <Form1>
        <Field
            type="checkbox" id={`${item.Id}chb`} name="IsCorrect"
            className="form-check-input"
            checked={item.IsCorrect}
        />
        <Field
            type="text" name="AnswerText" id={`${item.Id}txt`}
            placeholder={item.AnswerText}
        />
        <Button block size="lg" type="submit">
        Save
        </Button>
        <Button onClick={() => {axios.delete(`https://localhost:44327/api/Answer/?id=${item.Id}`)
      .then(res => {
        console.log(res);
        console.log(res.data);
      })}}>Delete</Button>
        </Form1>
        
    </Formik>));
        console.log({Answers});

        return(
        <div className="Question">
            <Button onClick={() => history.push(`/addanswer/${question.Id}`)}>Add answer</Button>
            <Form onSubmit={handleSubmit}>
            <Form.Group size="lg" controlId="name">
        <Form.Label><h3>Question</h3></Form.Label>
        <Form.Control
            type="text"
            defaultValue={question.QuestionText}
        onChange={(e) => setName(e.target.value)}
        />
        </Form.Group>
        <Button block size="lg" type="submit">
        Save
        </Button>
        </Form>
        <br />
        <br />
        <h6>Answers:</h6>
        {Answers}
        
        </div>
        )

        /* */
    
}           