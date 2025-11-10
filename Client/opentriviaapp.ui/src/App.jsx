import { useEffect, useState } from 'react'
import Question from './components/Question';
import axios from 'axios'

function App() {
    const [questions, setQuestions] = useState([])
    const [answers, setAnswers] = useState({})
    const [result, setResult] = useState(null)

    useEffect(() => {
        axios.get(`${import.meta.env.VITE_API_URL}/trivia/questions`)
            .then(res => setQuestions(res.data))
            .catch(err => console.error(err))
    }, [])

    const handleAnswer = (id, answer) => {
        setAnswers(prev => ({ ...prev, [id]: answer }))
    }

    const handleSubmit = () => {
        const payload = Object.entries(answers).map(([id, answer]) => ({
            id,
            selectedAnswer: answer
        }))
        axios.post(`${import.meta.env.VITE_API_URL}/trivia/checkanswers`, payload)
            .then(res => setResult(res.data))
            .catch(err => console.error(err))
    }

    return (
        <div className="container mt-5">
            <h1 className="mb-4">Trivia App</h1>
            {questions.map(q => (
                <Question key={q.id} question={q} onAnswer={handleAnswer} />
            ))}
            <button className="btn btn-primary mt-3" onClick={handleSubmit}>Submit Answers</button>
            {result && (
                <div className="alert alert-info mt-3">
                    You got {result.correctCount} out of {questions.length} correct.
                </div>
            )}
        </div>
    )
}

export default App