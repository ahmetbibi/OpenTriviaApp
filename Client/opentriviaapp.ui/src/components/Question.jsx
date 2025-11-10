import React from 'react'

function Question({ question, onAnswer }) {
    return (
        <div className="card mb-3">
            <div className="card-body">
                <h5 className="card-title" dangerouslySetInnerHTML={{ __html: question.question }}></h5>
                {question.options.map((opt, idx) => (
                    <div className="form-check" key={idx}>
                        <input
                            className="form-check-input"
                            type="radio"
                            name={question.id}
                            value={opt}
                            onChange={() => onAnswer(question.id, opt)}
                        />
                        <label className="form-check-label" dangerouslySetInnerHTML={{ __html: opt }}></label>
                    </div>
                ))}
            </div>
        </div>
    )
}

export default Question