'use client'
import { Button, Spinner } from 'flowbite-react'
import React, { useState } from 'react'
import { UpdateAuction } from '../actions/auctionActions';

export default function AuthText() {

    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState<{ status: number, message: string } | null>(null);

    function handleSubmit() {
        setResult(null);
        setLoading(true);
        UpdateAuction().then(res => setResult(res)).catch(err => setResult(err)).finally(() => setLoading(false))
    }

    return (
        <div>
            <Button onClick={handleSubmit}>
                {loading && <Spinner size='sm' className='me-3' light />}  Test Auth
            </Button>
            <div>
                {JSON.stringify(result, null, 2)}
            </div>
        </div>
    )
}
