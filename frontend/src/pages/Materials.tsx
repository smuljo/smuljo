import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Accordion, AccordionSummary, AccordionDetails, List, Typography } from '@mui/material';
import AddCommentModel from "../components/AddCommentModel";
import AddUniversityModal from "../components/AddUniversityModal";
import AddSubjectModel from "../components/AddSubjectModel";

export interface ReadSubjectTopicsItem {
    id: number;
    title: string;
}

export interface ReadUniversityTopicsItem {
    id: number;
    title: string;
}

export interface ReadCommentsItem {
    userName: string;
    text: string;
}

export interface PaginatedResponse<T> {
    items: T[]
    hasNext: boolean;
}

const Materials = () => {
    const [universities, setUniversities] = useState<ReadUniversityTopicsItem[]>([]);
    const [selectedUniversity, setSelectedUniversity] = useState<ReadUniversityTopicsItem | null>(null);
    const [subjects, setSubjects] = useState<ReadSubjectTopicsItem[]>([]);
    const [selectedSubject, setSelectedSubject] = useState<ReadSubjectTopicsItem | null>(null);
    const [comments, setComments] = useState<ReadCommentsItem[]>([]);
    const [selectedComment, setSelectedComment] = useState<ReadCommentsItem | null>(null);

    const address = `http://localhost:5000`;
    
    useEffect(() => {
        const fetchUniversities = async (itemsCount: number, page: number) => {
            try {
                const response = await axios.get<PaginatedResponse<ReadUniversityTopicsItem>>
                (`http://localhost:5000/api/topics/university?ItemsCount=${itemsCount}&Page=${page}`,
                    {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    });

                if (response.status === 200)
                    setUniversities(response.data.items);

                console.log(response);

            } catch (error) {
                console.error('Error fetching universities:', error);
            }
        };
        
        fetchUniversities(20, 1);
    }, []);

    const fetchSubjects = async (universityId: number, itemsCount: number, page: number) => {
        try {
            setSubjects([]);
            const response = await axios.get<PaginatedResponse<ReadSubjectTopicsItem>>
            (address + `/api/topics/subject?Id=${universityId}&ItemsCount=${itemsCount}&Page=${page}`);
            if (response.status === 200)
                setSubjects(response.data.items);
        } catch (error) {
            console.error('Error fetching subjects:', error);
        }
    };

    const fetchComments = async (subjectId: number, itemsCount: number, page: number) => {
        try {
            setComments([]);
            const response = await axios.get<PaginatedResponse<ReadCommentsItem>>
            (address + `/api/comments?Id=${subjectId}&ItemsCount=${itemsCount}&Page=${page}`);
            if (response.status === 200)
                setComments(response.data.items);
        } catch (error) {
            console.error('Error fetching comments:', error);
        }
    };
    
    const handleUniversityClick = (university: ReadUniversityTopicsItem) => {
        setSelectedUniversity(university);
        setSelectedSubject(null);
        setSelectedComment(null);
        fetchSubjects(university.id, 20, 1);
    };

    const handleSubjectClick = (subject: ReadSubjectTopicsItem) => {
        setSelectedSubject(subject);
        setSelectedComment(null);
        fetchComments(subject.id, 20, 1);
    };

    const handleCommentClick = (comment: ReadCommentsItem) => {
        setSelectedComment(comment);
    };

    if (universities)
    return (
        <div style={{ margin: '0 12%' }}>
            <List>
                {universities.map((university) => (
                    <Accordion key={university.id} expanded={selectedUniversity?.id === university.id}>
                        <AccordionSummary onClick={() => handleUniversityClick(university)}>
                            <Typography>{university.title}</Typography>
                        </AccordionSummary>
                        <AccordionDetails>
                            <List>
                                {subjects.map((subject) => (
                                    <Accordion key={subject.id} expanded={selectedSubject?.id === subject.id}>
                                        <AccordionSummary onClick={() => handleSubjectClick(subject)}>
                                            <Typography>{subject.title}</Typography>
                                        </AccordionSummary>
                                        <AccordionDetails>
                                             <List>
                                                {comments.map((comment) => (
                                                    <Accordion key={comment.text}
                                                               expanded={selectedComment?.text === comment.text}>
                                                        <AccordionSummary onClick={() => handleCommentClick(comment)}>
                                                            <Typography>{comment.userName}: {comment.text}</Typography>
                                                        </AccordionSummary>
                                                    </Accordion>
                                                ))}
                                                 <AddCommentModel topicId={subject.id} setComments={setComments}></AddCommentModel>
                                             </List>
                                        </AccordionDetails>
                                    </Accordion>
                                ))}
                                <AddSubjectModel mainTopicId={university.id} setSubjects={setSubjects}></AddSubjectModel>
                            </List>
                        </AccordionDetails>
                    </Accordion>
                ))}
                <AddUniversityModal setUniversities={setUniversities}></AddUniversityModal>
            </List>
        </div>
    );

    return (
        <div>Пока здесь ничего нет</div>
    )
};

export default Materials;
