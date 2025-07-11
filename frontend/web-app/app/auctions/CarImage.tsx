import Image from 'next/image'
import React, { useState } from 'react'

type Props = {
    auctionImageUrl: string
}

export default function CarImage({ auctionImageUrl }: Props) {

    const [loading, setLoading] = useState(false);
    return (
        <>
            <Image
                src={auctionImageUrl}
                alt="image"
                fill
                className={
                    `object-cover duration-700 ease-in-out ${loading ? 'opacity-0 scale-100' : 'opacity-100 scale-100'}`}
                sizes='(max-width:768px) 100vw, (max-width:1200px) 50vw'
                onLoad={() => setLoading(false)}
            />
        </>
    )
}
