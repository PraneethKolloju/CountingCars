import { auth } from '@/auth'
import React from 'react'
import AuthText from './AuthText';

export default async function Session() {
    var session = await auth();
    return (
        <>
            <div className='bg-blue-50 border-blue-600 border-2'>
                <h3 className='text-lg'>Session</h3>
                <pre>{JSON.stringify(session, null, 2)}</pre>
            </div>
            <AuthText />
        </>
    )
}
